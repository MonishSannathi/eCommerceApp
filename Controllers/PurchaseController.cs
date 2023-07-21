using Ecommerce.BusinessLayer.Implementation;
using Ecommerce.Models;
using Ecommerce.Models.Purchase;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

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
                Dictionary<string, string> messages = new Dictionary<string, string>();
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

        [HttpGet]
        public ActionResult ViewOrder(string id)
        {
           
            PurchaseOrderEntity purchaseOrderEntity = new PurchaseOrderEntity();
            purchaseOrderEntity = purchaseOrder.GetPurchaseOrder(id);
            if(purchaseOrderEntity == null) return RedirectToAction("DisplayError", "ErrorMessage", new { errorMessage = "Please Provide Valid File Details" });
            
            return View(purchaseOrderEntity);
        }

        [HttpGet]
        public ActionResult GetAllOrders()
        {
            List<PurchaseOrderEntity> purchaseOrderEntity = new List<PurchaseOrderEntity>();
            purchaseOrderEntity = purchaseOrder.GetPurchases();
            return View(purchaseOrderEntity);
        }

        
        public ActionResult DisplayPurchaseOrderPdf(string id, string path)
        {
            string filePath = purchaseOrder.ValidatePurchase(id, path);

            if (filePath == string.Empty) return View("Error");

            return File(filePath, "application/pdf");
        }

        [HttpGet]
        public ActionResult EditOrder(string id)
        {
            PurchaseOrderEntity purchaseOrderEntity = new PurchaseOrderEntity();
            purchaseOrderEntity = purchaseOrder.GetPurchaseOrder(id);
            if (purchaseOrderEntity == null) return RedirectToAction("DisplayError", "ErrorMessage", new { errorMessage = "Please Provide Valid File Details" });

            return View(purchaseOrderEntity);
        }

        [HttpPut]
        public ActionResult EditOrder(PurchaseOrderEntity purchaseOrderEntity)
        {
            if (!ModelState.IsValid) return View(purchaseOrderEntity);

            ResultStatus result = new ResultStatus();
            result = purchaseOrder.EditPurchaseOrder(purchaseOrderEntity);

            if (result.IsNotValid())
            {
                Dictionary<string, string> messages = new Dictionary<string, string>();
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

            return RedirectToAction("GetAllOrders");
        }

        [HttpDelete]
        public ActionResult DeleteOrder(string id)
        {
            ResultStatus result = new ResultStatus();
            result = purchaseOrder.DeletePurchaseOrder(id);

            if(result.IsNotValid())
            {
                return RedirectToAction("DisplayError", "ErrorMessage", new { errorMessage = result.GetErrorMessage() });
            }

            return RedirectToAction("GetAllOrders");
        }
    }
}