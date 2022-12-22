using HttpServer;
using LifeSafety.Models;
using LifeSafety.MVC;
using RazorEngineCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSafety.Controllers
{
    [Controller]
    public class SpecialServiceController
    {
        [Get]
        public string Index()
        {
            Articles[] articles;
            using (var context = new ArticlesContext())
            {
                articles = context.Articles.ToArray();
            }
            IRazorEngine razorEngine = new RazorEngine();
            string templateText = File.ReadAllText(Settings.SitePath + @"/index.html");

            var template = razorEngine.Compile<RazorEngineTemplateBase<Articles[]>>(templateText);

            string result = template.Run(instance => instance.Model = articles);

            return result;
        }
    }
}
