using Microsoft.Ajax.Utilities;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Ecommerce.Controllers
{
    public class ErrorMessageController : Controller
    {
        // GET: Error
        /*public ActionResult DisplayError()
        {
            ViewBag.ErrorMessage = "Application is not responding please try later";
            return View("DisplayError");
        }*/
        public ActionResult DisplayError(string errorMessage)
        {
            if (errorMessage.IsNullOrWhiteSpace() || errorMessage.IsEmpty()) errorMessage = "Application is not responding";

            ViewBag.ErrorMessage = errorMessage;
            return View("DisplayError");
        }
    }
}