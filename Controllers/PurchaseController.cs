using Ecommerce.BusinessLayer.Implementation;
using Ecommerce.Models;
using Ecommerce.Models.Purchase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
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
            else
            {

            }

            //System.IO.File.WriteAllBytes(fileName, encryptedData);

            return View();
        }

        public ActionResult ViewOrder()
        {
            PurchaseOrderEntity purchaseOrderEntity = new PurchaseOrderEntity();
         
            purchaseOrderEntity = purchaseOrder.GetPurchaseOrder();

            return View(purchaseOrderEntity);
        }

        public ActionResult GetAllOrders()
        {
            List<PurchaseOrderEntity> purchaseOrderEntity = new List<PurchaseOrderEntity>();
            purchaseOrderEntity = purchaseOrder.GetPurchases();
            return View(purchaseOrderEntity);
        }
    }
}