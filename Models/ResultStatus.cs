using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class ResultStatus
    {
        Boolean isNotValid;
        Dictionary<String, String> messages;
        Boolean shouldRedirectToError;
        String errorMessage;

        public ResultStatus()
        {
            messages = new Dictionary<string, string>();
        }

        public Dictionary<String, String> GetStatus()
        {
            return messages;
        }

        public void SetStatus(String key,String value)
        {
            messages[key] = value;
        }

        public Boolean IsNotValid()
        {
            return isNotValid;
        }

        public void SetNotValid(Boolean value)
        {
            isNotValid = value;
        }

        public Boolean ShouldRedirectToError() { return shouldRedirectToError; }
        public void setRedirectToError(Boolean value) { shouldRedirectToError = value; }

        public String GetErrorMessage() { return errorMessage; }
        public void SetErrorMessage(String value) {  errorMessage = value; }

    }
}