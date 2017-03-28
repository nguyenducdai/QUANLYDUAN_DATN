using System.Web.Mvc;
using System.Web.Routing;

namespace QLDuAn.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // truy cập trực tiếp vào file .axd không dc
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.IgnoreRoute("{*botdetect}", new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });
            routes.MapRoute(
                name: "Login",
                url: "dang-nhap.html",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                namespaces: new string[] { "QLDuAn.Web.Controllers" }
            );

            routes.MapRoute(
             name: "Default",
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
             namespaces: new string[] { "QLDuAn.Web.Controllers" }
         );
        }
    }
}