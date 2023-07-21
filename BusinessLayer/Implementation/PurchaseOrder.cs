using Ecommerce.BusinessLayer.Interfaces;
using Ecommerce.DatabaseLayer.Implementations;
using Ecommerce.DatabaseLayer.Interfaces;
using Ecommerce.Models;
using Ecommerce.Models.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Ecommerce.BusinessLayer.Implementation
{
    public class PurchaseOrder : IPurchase
    {
        private ILocalFileOperations localFileOperations;
        private IDatabase<PurchaseOrderEntity> database;
        private static Dictionary<string,string> purchases;
        public PurchaseOrder() 
        {
            database = new AppDatabase();
            if(purchases == null) purchases = new Dictionary<string, string>();
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
            
            ResultStatus result = new ResultStatus();

            try
            {
                localFileOperations = new AppFileOperations(purchaseOrderEntity.OrderFile.FileName);

                string filePath = localFileOperations.GetFilePath();

                //Save the file locally
                localFileOperations.SaveFile(filePath, purchaseOrderEntity.OrderFile);

                string encryptedfilePath = localFileOperations.GetEncryptedFilePath();
                localFileOperations.EncryptFile(filePath, encryptedfilePath);

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

        public PurchaseOrderEntity GetPurchaseOrder(string generatedId)
        {
            PurchaseOrderEntity purchaseOrderEntity = new PurchaseOrderEntity();
            try
            {
                purchaseOrderEntity = database.GetById(generatedId);

                localFileOperations = new AppFileOperations(purchaseOrderEntity.OrderPdfPath);

                string encryptedfilePath = localFileOperations.GetEncryptedFilePath();
                string decryptedfilePath = localFileOperations.GetDecryptedFilePath();

                localFileOperations.DecryptFile(encryptedfilePath, decryptedfilePath);
            }
            catch( Exception ex )
            {
                return null;
            }

            return purchaseOrderEntity;
        }

        public List<PurchaseOrderEntity> GetPurchases()
        {
            if (!purchases.Any()) InitializaPurchaseOrder();
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
            

            if (!purchases.ContainsKey(generatedId) || purchases[generatedId].CompareTo(filePath) != 0) return "";

            localFileOperations = new AppFileOperations(filePath);

            return localFileOperations.GetDecryptedFilePath();
        }

        public ResultStatus EditPurchaseOrder(PurchaseOrderEntity purchaseOrderEntity)
        {
            ResultStatus result = new ResultStatus();
            try
            {
                //If a new file is uploaded then save the file and encrypt it, delete the old files
                if(purchaseOrderEntity.OrderFile != null)
                {

                    localFileOperations = new AppFileOperations(purchaseOrderEntity.OrderFile.FileName);

                    string filePath = localFileOperations.GetFilePath();

                    //Save the file locally
                    localFileOperations.SaveFile(filePath, purchaseOrderEntity.OrderFile);

                    string encryptedfilePath = localFileOperations.GetEncryptedFilePath();
                    localFileOperations.EncryptFile(filePath, encryptedfilePath);

                    //Get the Old file name from the HashMap maintainted to validate the file
                    string oldFilePath = purchases[purchaseOrderEntity.id];

                    result = DeletePurchaseOrderFiles(oldFilePath);

                    if(result.IsNotValid())
                    {
                        return result;
                    }

                    //Set the New Pdf Path
                    purchaseOrderEntity.OrderPdfPath = purchaseOrderEntity.OrderFile.FileName;
                    //Update the Hash Map
                    purchases[purchaseOrderEntity.id] = purchaseOrderEntity.OrderPdfPath;

                }

                database.Update(purchaseOrderEntity);
            }
            catch (Exception ex)
            {
                result.SetNotValid(true);
                result.SetErrorMessage("There is something wrong with saving the file. Please try later");
            }

            return result;
        }

        public ResultStatus DeletePurchaseOrder(string id)
        {
            ResultStatus result = new ResultStatus();
            PurchaseOrderEntity purchaseOrderEntity = new PurchaseOrderEntity();
            try
            {
                purchaseOrderEntity = database.GetById(id);

                //First Delete the associated Files with Purchase Order
                result = DeletePurchaseOrderFiles(purchaseOrderEntity.OrderPdfPath);

                //If Success, only then delete the Purchase Order
                if (result.IsNotValid())
                {
                    result.SetNotValid(true);
                    return result;
                }

                database.Delete(purchaseOrderEntity);
            }
            catch (Exception ex)
            {
                result.SetNotValid(true);
                result.SetErrorMessage("Something went wrong During Deletion. Please try again later");
            }

            return result;
        }

        public ResultStatus DeletePurchaseOrderFiles(string fileName)
        {
            ResultStatus result = new ResultStatus();
            
            try
            {                
                localFileOperations = new AppFileOperations(fileName);

                string filePath = localFileOperations.GetFilePath();
                string encryptedfilePath = localFileOperations.GetEncryptedFilePath();
                string decryptedfilePath = localFileOperations.GetDecryptedFilePath();

                //Delete all three files if exists
                //Original File -- Encrypted File -- DecryptedFile

                if (!localFileOperations.DeleteFile(filePath) || !localFileOperations.DeleteFile(encryptedfilePath) || !localFileOperations.DeleteFile(decryptedfilePath))
                {
                    result.SetNotValid(true);
                    result.SetErrorMessage("Unable to Delete Purchase Order Files. Please try again later");
                }
            }
            catch (Exception ex)
            {
                result.SetNotValid(true);
                result.SetErrorMessage("Something went wrong while Deletion. Please try again later");
            }

            return result;
        }
    }
}