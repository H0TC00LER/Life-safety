using LifeSafety.MVC;

namespace LifeSafety
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var prefix = @"http://localhost:8080/";
            var sitePath = "./site/site";
            var server = new Server(prefix, sitePath);
            await server.StartServer();
        }
    }
}