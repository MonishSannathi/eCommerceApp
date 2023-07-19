using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class ErrorMessageController : Controller
    {
        // GET: Error
        public ActionResult DisplayError(string errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;
            return View("DisplayError");
        }
    }
}