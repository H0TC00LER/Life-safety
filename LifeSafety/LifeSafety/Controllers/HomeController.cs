using HttpServer;
using LifeSafety.Models;
using LifeSafety.MVC;
using Microsoft.EntityFrameworkCore;
using RazorEngineCore;

namespace LifeSafety.Controllers
{
    [Controller]
    public class HomeController
    {
        [Get]
        public string Index()
        {
            Articles[] articles;
            using(var context = new ArticlesContext())
            {
                articles = context.Articles.ToArray();
            }
            IRazorEngine razorEngine = new RazorEngine();
            string templateText = File.ReadAllText(Settings.SitePath + @"/home/index.html");

            var template = razorEngine.Compile<RazorEngineTemplateBase<Articles[]>>(templateText);

            string result = template.Run(instance => instance.Model = articles);

            return result;
        }

        [Get]
        public async Task<string> ShowArticle(int id)
        {
            Articles? article;
            using (var context = new ArticlesContext())
            {
                article = await context.Articles.FirstOrDefaultAsync(a => a.ArticleId == id);
            }
            IRazorEngine razorEngine = new RazorEngine();
            string templateText = await File.ReadAllTextAsync(Settings.SitePath + @"/home/article.html");

            var template = razorEngine.Compile<RazorEngineTemplateBase<Articles>>(templateText);

            string result = template.Run(instance => instance.Model = article!);

            return result;
        }
    }
}
