using Ecommerce.BusinessLayer.Interfaces;
using System;
using System.IO;
using System.Web;
using System.Web.Hosting;

namespace Ecommerce.BusinessLayer.Implementation
{
    public class AppFileOperations : ILocalFileOperations
    {
        private string fileExtension;
        private string fileNameWithoutExtension;
        private IEncryption encryption;

        public AppFileOperations(string path)
        {
            encryption = new CommonEncryption();
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

        public void SaveFile(string path, HttpPostedFileBase httpPostedFile) 
        {
            httpPostedFile.SaveAs(path);
        }

        public bool DeleteFile(string path)
        {
            try
            {
                if(File.Exists(path)) { File.Delete(path); }
            }
            catch (Exception ex){
                return false;
            }
            
            return true;
        }

        public void EncryptFile(string inputFilePath,string outputFilePath)
        {
            //If the encrypted file already exists then do not run the encryption again
            if (!File.Exists(outputFilePath))
            {
                encryption.Encrypt(inputFilePath, outputFilePath);
            }
        }

        public void DecryptFile(string inputFilePath, string outputFilePath)
        {
            //If the decrypted file already exists then do not run the decryption again
            if(!File.Exists(outputFilePath))
            {
                encryption.Decrypt(inputFilePath, outputFilePath);
            }
        }
    }
}