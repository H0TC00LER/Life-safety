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
    public class LoginController
    {
        [Get]
        public string Index()
        {
            return File.ReadAllText(Settings.SitePath + @"/login/index.html");
        }

        [Post]
        public void RegisterUser(string login, string password)
        {

        }
    }
}
