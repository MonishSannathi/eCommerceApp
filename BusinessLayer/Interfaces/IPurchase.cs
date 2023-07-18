using Ecommerce.BusinessLayer.Implementation;
using Ecommerce.Models;
using Ecommerce.Models.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BusinessLayer.Interfaces
{
    internal interface IPurchase
    {
        ResultStatus AddPurchaseOrder(PurchaseOrderEntity purchaseOrderEntity);
        PurchaseOrderEntity GetPurchaseOrder();

        List<PurchaseOrderEntity> GetPurchases();

        ResultStatus DeletePurchase();
    }
}
