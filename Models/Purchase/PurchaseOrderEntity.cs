using Ecommerce.CustomAttributes.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.Models.Purchase
{
    public class PurchaseOrderEntity
    {
        public string id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name must be at most 10 characters.")]
        public string OrderTitle { get; set; }

        
        [DisplayName("UploadFile")]
        public string OrderPdfPath { get; set; }

        [ValidFile]
        public HttpPostedFileBase OrderFile { get; set; }
    }
}