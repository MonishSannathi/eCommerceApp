using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ecommerce
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Purchase", action = "GetAllOrders", id = UrlParameter.Optional }, 
                constraints: new { httpMethod = new HttpMethodConstraint(new[] { "get" ,"post" }) }
            );

            routes.MapRoute(
                name: "Delete Methods",
                url: "{version}/cards/{cardID}",
                defaults: new { Controller = "Purchase", Action = "DeleteOrder" },
                constraints: new { httpMethod = new HttpMethodConstraint(new[] { "delete" }) }
            );
        }
    }
}
