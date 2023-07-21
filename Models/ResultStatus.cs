using System.Collections.Generic;

namespace Ecommerce.Models
{
    public class ResultStatus
    {
        bool isNotValid;
        Dictionary<string, string> messages;
        bool shouldRedirectToError;
        string errorMessage;

        public ResultStatus()
        {
            messages = new Dictionary<string, string>();
        }

        public Dictionary<string, string> GetStatus()
        {
            return messages;
        }

        public void SetStatus(string key,string value)
        {
            messages[key] = value;
        }

        public bool IsNotValid()
        {
            return isNotValid;
        }

        public void SetNotValid(bool value)
        {
            isNotValid = value;
        }

        public bool ShouldRedirectToError() { return shouldRedirectToError; }
        public void setRedirectToError(bool value) { shouldRedirectToError = value; }

        public string GetErrorMessage() { return errorMessage; }
        public void SetErrorMessage(string value) {  errorMessage = value; }

    }
}