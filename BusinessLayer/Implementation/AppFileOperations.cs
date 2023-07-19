using Ecommerce.BusinessLayer.Interfaces;
using Ecommerce.Models.Purchase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Ecommerce.BusinessLayer.Implementation
{
    public class AppFileOperations : ILocalFileOperations
    {
        private string fileExtension;
        private string fileNameWithoutExtension;

        public AppFileOperations()
        {
           
        }

        public AppFileOperations(string path)
        {
            this.fileExtension = Path.GetExtension(path);
            this.fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
        }

        public string GetAbsoluteFilePath()
        {
            string fileName = this.fileNameWithoutExtension + this.fileExtension;

            string filePath = Path.Combine(HostingEnvironment.MapPath("~/PurchaseOrderFiles/"), fileName);

            return filePath;
        }

        public string GetRelativeFilePath()
        {
            string fileName = this.fileNameWithoutExtension + this.fileExtension;

            string filePath = Path.Combine("~/PurchaseOrderFiles/", fileName);

            return filePath;
        }



        public string GetDecryptedFilePath()
        {
            string decryptedFileNameWithoutExtension = this.fileNameWithoutExtension + "_decrypted_";
            string decryptedFileName = decryptedFileNameWithoutExtension + this.fileExtension;
            string decryptedFilePath = Path.Combine(HostingEnvironment.MapPath("~/PurchaseOrderFiles/"), decryptedFileName);
            return decryptedFilePath;
        }

        public string GetEncryptedFilePath()
        {
            string encryptedFileNameWithoutExtension = this.fileNameWithoutExtension + "_encrypted_";
            string encryptedFileName = encryptedFileNameWithoutExtension+this.fileExtension;
            string encryptedFilePath = Path.Combine(HostingEnvironment.MapPath("~/PurchaseOrderFiles/"), encryptedFileName);
            return encryptedFilePath;
        }

        public string GetDecryptedLocalFilePath()
        {
            string decryptedFileNameWithoutExtension = this.fileNameWithoutExtension + "_decrypted_";
            string decryptedFileName = decryptedFileNameWithoutExtension + this.fileExtension;
            return "/PurchaseOrderFiles/" + decryptedFileName;
        }
        public FileResult GetFile()
        {
            throw new NotImplementedException();
        }


        public void SaveFile(string path, HttpPostedFileBase httpPostedFile) 
        {
            httpPostedFile.SaveAs(path);
        }


        public void DeleteFile()
        {
            throw new NotImplementedException();
        }

        public void SetFileDetails(string path)
        {
            this.fileExtension = Path.GetExtension(path);
            this.fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
        }
    }
}