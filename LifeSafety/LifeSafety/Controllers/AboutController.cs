using HttpServer;
using LifeSafety.Models;
using LifeSafety.MVC;
using RazorEngineCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace LifeSafety.Controllers
{
    [Controller]
    public class AboutController
    {
        [Get]
        public string Index()
        {
            return File.ReadAllText(Settings.SitePath + @"/about/index.html");
        }
    }
}
