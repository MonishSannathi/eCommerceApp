using Ecommerce.DatabaseLayer.Interfaces;
using Ecommerce.Models.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.DatabaseLayer.Implementations
{
    public class AppDatabase : IDatabase<PurchaseOrderEntity>
    {
        private static List<PurchaseOrderEntity> _purchaseOrders;

        public AppDatabase() { 
            if(null == _purchaseOrders)
            {
                _purchaseOrders = new List<PurchaseOrderEntity>();
            }
        }
        public void Add(PurchaseOrderEntity entity)
        {
            _purchaseOrders.Add(entity);
        }

        public void Delete(PurchaseOrderEntity entity)
        {
            _purchaseOrders.Remove(entity);
        }

        public PurchaseOrderEntity GetById(int id)
        {
            return _purchaseOrders.ElementAt(id);
        }

        public void Update(PurchaseOrderEntity entity)
        {
            int index = _purchaseOrders.FindIndex(x => x.id == entity.id);
            _purchaseOrders[index] = entity;
        }


        public List<PurchaseOrderEntity> GetAll() {  return _purchaseOrders; }
    }
}