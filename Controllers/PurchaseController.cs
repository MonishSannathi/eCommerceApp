using Ecommerce.BusinessLayer.Implementation;
using Ecommerce.Models;
using Ecommerce.Models.Purchase;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Ecommerce.Controllers
{
    public class PurchaseController : Controller
    {
        private PurchaseOrder purchaseOrder;

        public PurchaseController()
        {
            purchaseOrder = new PurchaseOrder();
        }
        [HttpGet]
        public ActionResult AddOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddOrder(PurchaseOrderEntity purchaseOrderEntity)
        {
            if(!ModelState.IsValid) return View(purchaseOrderEntity);

            ResultStatus result = new ResultStatus();
            result = purchaseOrder.AddPurchaseOrder(purchaseOrderEntity);

            if (result.IsNotValid())
            {
                Dictionary<String, String> messages = new Dictionary<string, string>();
                messages = result.GetStatus();
                foreach (KeyValuePair<string, string> entry in messages)
                {
                    ModelState.AddModelError(entry.Key, entry.Value);

                }
            }
            else if (result.ShouldRedirectToError())
            {
                return RedirectToAction("DisplayError", "ErrorMessage", new { errorMessage = result.GetErrorMessage() });
            }
            else
            {
                //Clear the Values in the form once successful Submission
                ModelState.Clear();
            }
            
            return View();
        }

        public ActionResult ViewOrder(string id)
        {
           
            PurchaseOrderEntity purchaseOrderEntity = new PurchaseOrderEntity();
            purchaseOrderEntity = purchaseOrder.GetPurchaseOrder(id);
            if(purchaseOrderEntity == null) return RedirectToAction("DisplayError", "ErrorMessage", new { errorMessage = "Please Provide Valid File Details" });
            
            return View(purchaseOrderEntity);
        }

        public ActionResult GetAllOrders()
        {
            List<PurchaseOrderEntity> purchaseOrderEntity = new List<PurchaseOrderEntity>();
            purchaseOrderEntity = purchaseOrder.GetPurchases();
            return View(purchaseOrderEntity);
        }

        [CustomAttributes.ValidateUser]
        public ActionResult DisplayPurchaseOrderPdf(string id, string path)
        {
            /*if (string.IsNullOrEmpty(id)) return "";
            else if()*/
            //Get the filePath from the id

            string filePath = purchaseOrder.ValidatePurchase(id, path);

            if (filePath == string.Empty) return View("Error");

            return File(Server.MapPath(filePath), "application/pdf");
        }
    }
}