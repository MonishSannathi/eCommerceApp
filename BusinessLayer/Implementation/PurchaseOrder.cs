using Ecommerce.BusinessLayer.Interfaces;
using Ecommerce.DatabaseLayer.Implementations;
using Ecommerce.DatabaseLayer.Interfaces;
using Ecommerce.HelperMethods;
using Ecommerce.Models;
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
    public class PurchaseOrder : IPurchase
    {
        private ILocalFileOperations localFileOperations;
        private IDatabase<PurchaseOrderEntity> database;
        public PurchaseOrder() 
        {
            database = new AppDatabase();
            localFileOperations = new AppFileOperations();
        }
        public ResultStatus AddPurchaseOrder(PurchaseOrderEntity purchaseOrderEntity)
        {

            //First Add File to the Local Path
            localFileOperations.SetFileDetails(purchaseOrderEntity.OrderFile.FileName);

            ResultStatus result = new ResultStatus();


            if (!localFileOperations.CheckFileName())
            {
                result.SetNotValid(true);
                result.SetStatus(Utility.GetPropertyName<PurchaseOrderEntity>(x => x.OrderPdfPath), "File Name exceeds the Limit size");
            }

            if (!localFileOperations.CheckFileExtension(purchaseOrderEntity.OrderFile.ContentType))
            {
                result.SetNotValid(true);
                result.SetStatus(Utility.GetPropertyName<PurchaseOrderEntity>(x => x.OrderPdfPath), "File Extension is Invalid");

                
            }

            if (!localFileOperations.CheckFileSize(purchaseOrderEntity.OrderFile.ContentLength))
            {
                result.SetNotValid(true);
                result.SetStatus(Utility.GetPropertyName<PurchaseOrderEntity>(x => x.OrderPdfPath), "File Content exceeds the Limit");

                
            }


            //If not valid then do not do encryption
            if (result.IsNotValid()) return result;


            string filePath = localFileOperations.GetFilePath();
            localFileOperations.SaveFile(filePath,purchaseOrderEntity.OrderFile);


            string encryptedfilePath = localFileOperations.GetEncryptedFilePath();

            
            CommonEncryption commonEncryption = new CommonEncryption();
            commonEncryption.EncryptFile(filePath, encryptedfilePath);

            //Add it to the Database Layer
            purchaseOrderEntity.id = Guid.NewGuid().ToString();
            database.Add(purchaseOrderEntity);

            //After Encryption Add File to the File System Server
            //Yet to Do
            return result;
        }

        public ResultStatus DeletePurchase()
        {
            throw new NotImplementedException();
        }

        public PurchaseOrderEntity GetPurchaseOrder()
        {
            //From the Server yet to do
            //fileOperations.GetFile();
            PurchaseOrderEntity purchaseOrderEntity = new PurchaseOrderEntity();
            purchaseOrderEntity.OrderTitle = "Hello";
            purchaseOrderEntity.OrderPdfPath = "9780735667457_samplechapters.pdf";

            localFileOperations = new AppFileOperations(purchaseOrderEntity.OrderPdfPath);

            string encryptedfilePath = localFileOperations.GetEncryptedFilePath();
            string decryptedfilePath = localFileOperations.GetDecryptedFilePath();

           
            CommonEncryption commonEncryption = new CommonEncryption();
            commonEncryption.DecryptFile(encryptedfilePath, decryptedfilePath);



            // Save the decrypted data as a file
            //System.IO.File.WriteAllBytes(decryptedFilePath, decryptedData);

            // Provide the decrypted file for download
            //return File(decryptedFilePath, "application/octet-stream", decryptedFileName + ".pdf");

            purchaseOrderEntity.OrderPdfPath = localFileOperations.GetDecryptedLocalFilePath();

            return purchaseOrderEntity;
        }

        public List<PurchaseOrderEntity> GetPurchases()
        {

            List<PurchaseOrderEntity> purchaseOrderEntities = database.GetAll()
             .Where(obj => obj is PurchaseOrderEntity)
             .OfType<PurchaseOrderEntity>()
             .ToList();

            return purchaseOrderEntities;
        }
    }
}