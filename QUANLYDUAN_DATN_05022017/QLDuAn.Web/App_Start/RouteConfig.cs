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
                name: "doanhthu",
                url: "thong-ke.html",
                defaults: new { controller = "DuAn", action = "ThongKe", id = UrlParameter.Optional },
                namespaces: new string[] { "QLDuAn.Web.Controllers" }
            );

            routes.MapRoute(
                name: "updateProfile",
                url: "cap-nhat-thong-tin.html",
                defaults: new { controller = "Account", action = "CapNhatThongTin", id = UrlParameter.Optional },
                namespaces: new string[] { "QLDuAn.Web.Controllers" }
            );

            routes.MapRoute(
              name: "profile",
              url: "thong-tin-ca-nhan.html",
              defaults: new { controller = "Account", action = "ThongTinCanhan", id = UrlParameter.Optional },
              namespaces: new string[] { "QLDuAn.Web.Controllers" }
              ); 

            routes.MapRoute(
                name: "Login",
                url: "dang-nhap.html",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                namespaces: new string[] { "QLDuAn.Web.Controllers" }
            );

            routes.MapRoute(
                 name: "HomeSite",
                 url: "account/home",
                 defaults: new { controller = "Home", action = "Site", id = UrlParameter.Optional },
                 namespaces: new string[] { "QLDuAn.Web.Controllers" }
            );

            routes.MapRoute(
                 name: "DetailAlert",
                 url: "thong-bao/chi-tiet-{id}.html",
                 defaults: new { controller = "Home", action = "Detail", id = UrlParameter.Optional },
                 namespaces: new string[] { "QLDuAn.Web.Controllers" }
             );

            routes.MapRoute(
               name: "HangMucCongViec",
               url: "hang-muc-cong-viec.html",
               defaults: new { controller = "DuAn", action = "HangMucCongViec", id = UrlParameter.Optional },
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