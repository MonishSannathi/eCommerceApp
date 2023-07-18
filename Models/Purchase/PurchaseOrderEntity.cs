using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Ecommerce.Models.Purchase
{
    public class PurchaseOrderEntity
    {
        public string id { get; set; }
        public string OrderTitle { get; set; }

        [DisplayName("UploadFile")]
        public string OrderPdfPath { get; set; }

        public HttpPostedFileBase OrderFile { get; set; }
    }
}