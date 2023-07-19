using Ecommerce.BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.BusinessLayer.Implementation
{
    
    public class ExternalServerFileOperations : IExternalFileOperations
    {
        public string GetFilePath()
        {
            //Do Authentication
            // And then add it to server
            throw new NotImplementedException();
        }

        public void DeleteFile()
        {
            throw new NotImplementedException();
        }

        public string GetDecryptedFilePath()
        {
            throw new NotImplementedException();
        }

        public string GetDecryptedLocalFilePath()
        {
            throw new NotImplementedException();
        }

        public string GetEncryptedFilePath()
        {
            throw new NotImplementedException();
        }

        public FileResult GetFile()
        {
            //Do Authentication
            //Once you get the file add 
            throw new NotImplementedException();
        }

        public void SaveFile(string path, HttpPostedFileBase file)
        {
            throw new NotImplementedException();
        }

        public string GetAbsoluteFilePath()
        {
            throw new NotImplementedException();
        }

        public string GetRelativeFilePath()
        {
            throw new NotImplementedException();
        }
    }
}