using LifeSafety.MVC;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace LifeSafety
{
    public class Server
    {
        private readonly HttpListener httpListener;
        private HttpListenerContext? httpContext;
        private readonly string prefix;
        private readonly string sitePath;
        private HttpListenerRequest? request;
        private HttpListenerResponse? response;
        private string? rawUrl;

        private readonly Dictionary<string, string> ContentTypes = new Dictionary<string, string>
        {
            {"png", @"image/png" },
            {"css", @"text/css" },
            {"html", @"text/html" }
        };

        public Server(string prefix, string sitePath)
        {
            httpListener = new HttpListener();
            this.prefix = prefix;
            this.sitePath = sitePath;
        }

        public async Task StartServer()
        {
            httpListener.Prefixes.Add(prefix);
            httpListener.Start();

            while (true)
            {
                httpContext = await httpListener.GetContextAsync();

                request = httpContext.Request;
                response = httpContext.Response;
                rawUrl = request.RawUrl;

                Console.WriteLine(rawUrl);
                Console.WriteLine(request.HttpMethod);

                var buffer = await HandleRequest();

                response.ContentLength64 = buffer.Length;
                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }

        public async Task<byte[]> HandleRequest()
        {
            var result = await GetFilesAsync();
            if (result != null)
                return result;

            result = await GetAction();
            if (result != null)
                return result;

            response!.Headers.Set("Content-type", "text/plain");

            response.StatusCode = (int)HttpStatusCode.NotFound;
            string error = "error 404 - not found";

            return Encoding.UTF8.GetBytes(error);
        }

        public async Task<byte[]?> GetFilesAsync()
        {
            byte[]? buffer = null;
            var filePath = sitePath + rawUrl;

            //if (Directory.Exists(filePath))
            //{
            //    filePath += "/index.html";
            //    response!.Headers.Set("Content-type", "text/html");
            //    response.StatusCode = (int)HttpStatusCode.OK;
            //    if (File.Exists(filePath))
            //        buffer = await File.ReadAllBytesAsync(filePath);
            //}
            if (File.Exists(filePath))
            {
                buffer = await File.ReadAllBytesAsync(filePath);
                var fileType = rawUrl!.Substring(rawUrl.IndexOf('.') + 1);
                response!.Headers.Set("Content-type", ContentTypes[fileType]);
                response!.StatusCode = (int)HttpStatusCode.OK;
            }
            return buffer;
        }

        public async Task<byte[]?> GetAction()
        {
            var splittedRawUrl = rawUrl!.Split("/").Skip(1).ToArray();

            var controller = GetController(splittedRawUrl[0]);
            if (controller == null)
                return null;

            var method = GetMethod(controller, splittedRawUrl.Length < 2 ? "" : splittedRawUrl[1]);
            if (method == null)
                return null;

            object? methodResult = null;
            if (request!.HttpMethod == "GET")
            {
                var getParams = request.QueryString.Keys
                    .Cast<string>()
                    .Select(k => request.QueryString[k])
                    .ToArray();

                methodResult = method.Invoke(Activator.CreateInstance(controller), getParams);
            }
            else
            {
                //Dictionary<string, string> postParams = new Dictionary<string, string>();
                //string[] rawParams = ReadRequestBody(request.InputStream).Split('&');
                //foreach (string param in rawParams)
                //{
                //    string[] kvPair = param.Split('=');
                //    string key = kvPair[0];
                //    string value = HttpUtility.UrlDecode(kvPair[1]);
                //    postParams.Add(key, value);
                //}
                var requestBody = await ReadRequestBody(request.InputStream);
                var rawParams = requestBody.Split('&');
                var postParams = rawParams.Select(s => s.Split('=')[1]).ToArray();
                methodResult = method.Invoke(Activator.CreateInstance(controller), postParams);
            }

            response!.Headers.Set("Content-type", "text/html");
            byte[] buffer = Encoding.UTF8.GetBytes(methodResult!.ToString()!);
            return buffer;
        }

        private Type? GetController(string controllerName)
        {
            return Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(ControllerAttribute), false).Length > 0)
                .Where(t => t.Name.ToLower() == controllerName + "controller")
                .FirstOrDefault();
        }

        private MethodInfo? GetMethod(Type controller, string methodName)
        {
            var methodAttribute = request!.HttpMethod == "GET" ? typeof(GetAttribute) : typeof(PostAttribute);

            return controller
                .GetMethods()
                .Where(t => t.GetCustomAttributes(methodAttribute, false).Length > 0)
                .Where(m => methodName == "" ? m.Name == "Index" : m.Name == methodName)
                .FirstOrDefault();
        }

        private async Task<string> ReadRequestBody(Stream inputStream)
        {
            string documentContents;
            using (Stream receiveStream = inputStream)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    documentContents = await readStream.ReadToEndAsync();
                }
            }
            return documentContents;
        }
    }
}
