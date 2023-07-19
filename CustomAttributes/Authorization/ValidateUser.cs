using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.CustomAttributes
{
    public class ValidateUser:AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Get the value of the cookie
            HttpCookie cookie = httpContext.Request.Cookies["Auth-Token"];
            

            // Check if the cookie exists and has a valid value
            if (cookie != null && IsValidToken(cookie.Value))
            {
                return true; // Access is granted
            }

            return false; // Access is denied
        }

        private bool IsValidToken(string token)
        {

            //compare the token with a hardcoded value.
            return token.CompareTo("secretkey")==0;
        }
    }
}