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
        private string[] allowedExtensions = { ".pdf" };
        private int allowedContentLength = 5000000;
        private int allowedNameLength = 100;
        public AppFileOperations()
        {
           
        }

        public AppFileOperations(string path)
        {
            this.fileExtension = Path.GetExtension(path);
            this.fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
        }

        public string GetFilePath()
        {
            string fileName = this.fileNameWithoutExtension + this.fileExtension;

            string filePath = Path.Combine(HostingEnvironment.MapPath("~/PurchaseOrderFiles/"), fileName);

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

        public Boolean CheckFileName()
        {
            //Check Whether the fileName exceeds the string length
            if(this.fileNameWithoutExtension.Length > this.allowedNameLength)
            {
                return false;
            }
            return true;
        }

        public Boolean CheckFileExtension(string contentType)
        {
            //Check Whether the fileExtension is expected or not 
            //Also check Content type
            if(!allowedExtensions.Contains(this.fileExtension) || contentType.ToLower().CompareTo("application/pdf") != 0) return false;
            return true;
        }

        public Boolean CheckFileSize(int contentLength)
        {
            //Check if file content length is greater than Half the mega byte
            if (contentLength > this.allowedContentLength)
            {
                return false;
            }
            return true;
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