using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace E_Store.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null,
                 "",
                 new
                 {
                     controller = "Product",
                     action = "List",
                     category = (string)null,
                     page = 1
                 }
             );

            routes.MapRoute(
                name: null,
                url: "Page{page}",
                defaults: new { controller = "Product", action = "List", category = (string)null },
                constraints: new { page = @"\d+" }
            );

            routes.MapRoute(null,
               "admin/{action}",
               new { controller = "Admin", action = "index" }
           );

            routes.MapRoute(null,
              "cart/{action}",
              new { controller = "Cart", action = "index" }
          );


            routes.MapRoute(null,
                "{category}/{subCategory}",
                new { controller = "Product", action = "List", page = 1, subCategory=(string)null }
            );

            routes.MapRoute(null,
                "{category}/Page{page}",
                new { controller = "Product", action = "List" },
                new { page = @"\d+" }
            );
            routes.MapRoute(null,
                "Product/{action}/{productId}",
                new {controller="Admin" },
                new { productId = @"\d+" }
                );
            
            routes.MapRoute(null, "{controller}/{action}");
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
