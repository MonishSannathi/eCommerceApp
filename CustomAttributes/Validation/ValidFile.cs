using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Ecommerce.CustomAttributes.Validation
{
    public class ValidFile:ValidationAttribute
    {
        private int allowedContentLength = 10000000;
        private int allowedNameLength = 100;
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as HttpPostedFileBase;

            if(file != null)
            {
                if (file.FileName.Length > this.allowedNameLength) { return new ValidationResult("File Name exceeds the Limit size"); }
                if (file.ContentType.ToLower().CompareTo("application/pdf") != 0) { return new ValidationResult("File should be in pdf format"); }
                if (file.ContentLength > this.allowedContentLength) { return new ValidationResult("File Content exceeds the Limit"); }
            }

            

            return ValidationResult.Success;
        }
    }
}