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
        private Dictionary<string,string> purchases;
        public PurchaseOrder() 
        {
            database = new AppDatabase();
            purchases = new Dictionary<string,string>();
        }

        /*
            Maintain the File Unique id of file and Path as Key Value Pair
            This will be used to Validate the file request
        */
        private void InitializaPurchaseOrder()
        {
            purchases = database.GetAll().ToDictionary(x => x.id, x => x.OrderPdfPath);
        }


        /*
            Adds the Purchase Order in the Database
        */
        public ResultStatus AddPurchaseOrder(PurchaseOrderEntity purchaseOrderEntity)
        {
            
            localFileOperations = new AppFileOperations();
            localFileOperations.SetFileDetails(purchaseOrderEntity.OrderFile.FileName);
            
            ResultStatus result = new ResultStatus();


            try
            {
                string filePath = localFileOperations.GetAbsoluteFilePath();

                //Save the file locally
                localFileOperations.SaveFile(filePath, purchaseOrderEntity.OrderFile);


                string encryptedfilePath = localFileOperations.GetEncryptedFilePath();



                CommonEncryption commonEncryption = new CommonEncryption();
                commonEncryption.EncryptFile(filePath, encryptedfilePath);

                //Add it to the Database Layer
                purchaseOrderEntity.id = Guid.NewGuid().ToString();
                purchaseOrderEntity.OrderPdfPath = purchaseOrderEntity.OrderFile.FileName;

                //Add the id to the Dictionary to keep track
                purchases.Add(purchaseOrderEntity.id, purchaseOrderEntity.OrderPdfPath);
                database.Add(purchaseOrderEntity);
            }
            catch(Exception ex)
            {
                result.setRedirectToError(true);
                result.SetErrorMessage("There is something wrong with saving the file. Please try later");
            }

            return result;
        }

        public ResultStatus DeletePurchase()
        {
            throw new NotImplementedException();
        }

        public PurchaseOrderEntity GetPurchaseOrder(string generatedId)
        {
            PurchaseOrderEntity purchaseOrderEntity = new PurchaseOrderEntity();
            try
            {
                purchaseOrderEntity = database.GetById(generatedId);

                localFileOperations = new AppFileOperations(purchaseOrderEntity.OrderPdfPath);

                string encryptedfilePath = localFileOperations.GetEncryptedFilePath();
                string decryptedfilePath = localFileOperations.GetDecryptedFilePath();


                CommonEncryption commonEncryption = new CommonEncryption();
                commonEncryption.DecryptFile(encryptedfilePath, decryptedfilePath);
            }
            catch( Exception ex )
            {
                return null;
            }

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

        public string ValidatePurchase(string generatedId, string filePath)
        {
            //If the generatedId not Present
            //If the generatedId is not associated to the file path
            //Both the above cases are invalid
            if (!purchases.Any()) InitializaPurchaseOrder();

            if (!purchases.ContainsKey(generatedId) || purchases[generatedId].CompareTo(filePath) != 0) return "";

            localFileOperations = new AppFileOperations(filePath);

            return localFileOperations.GetDecryptedLocalFilePath();
        }
    }
}