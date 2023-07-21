using Ecommerce.Models;
using Ecommerce.Models.Purchase;
using System.Collections.Generic;

namespace Ecommerce.BusinessLayer.Interfaces
{
    internal interface IPurchase
    {
        ResultStatus AddPurchaseOrder(PurchaseOrderEntity purchaseOrderEntity);
        PurchaseOrderEntity GetPurchaseOrder(string generatedId);

        List<PurchaseOrderEntity> GetPurchases();

        ResultStatus DeletePurchaseOrder(string id);

        string ValidatePurchase(string generatedId,string filePath);

        ResultStatus EditPurchaseOrder(PurchaseOrderEntity purchaseOrderEntity);
    }
}
