using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Ecommerce
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static bool AuthTokenAdded = false;
        protected void Application_Start()
        {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            /*To mimc the login the below code has been added temporarily*/
            if (!AuthTokenAdded)
            {
                AuthTokenAdded = true;

                HttpCookie cookie = new HttpCookie("Auth-Token");
                cookie.Value = "secretkey";
                cookie.Expires = DateTime.Now.AddMinutes(60); 

                // Add the cookie to the response
                Response.Cookies.Add(cookie);
             
            }
        }
    }
}
