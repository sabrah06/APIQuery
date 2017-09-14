using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace APIQuery
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Register",
                url: "register",
                defaults: new { controller = "register", action = "index", searchparam = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "RegisterSave",
               url: "register/save",
               defaults: new { controller = "register", action = "save", searchparam = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "Search",
                url: "api/searchrecipe/{searchparam}",
                defaults: new { controller = "api", action = "searchrecipe", searchparam = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "home", action = "index", id = UrlParameter.Optional }
            );
        }
    }
}
