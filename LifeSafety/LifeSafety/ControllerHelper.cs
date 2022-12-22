using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LifeSafety
{
    public static class ControllerHelper
    {
        //public static async Task<byte[]> ShowTemplateAsync(HttpListenerResponse response, string filePath)
        //{
        //    byte[] buffer;
        //    response!.Headers.Set("Content-type", "text/html");
        //    response.StatusCode = (int)HttpStatusCode.OK;
        //    if (File.Exists(filePath))
        //        buffer = await File.ReadAllBytesAsync(filePath);

        //    response.ContentLength64 = buffer.Length;
        //    Stream output = response.OutputStream;
        //    output.Write(buffer, 0, buffer.Length);
        //    output.Close();
        //}
    }
}
