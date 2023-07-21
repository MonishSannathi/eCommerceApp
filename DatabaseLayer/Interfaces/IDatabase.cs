using System.Collections.Generic;

namespace Ecommerce.DatabaseLayer.Interfaces
{
    internal interface IDatabase<T>
    {
        T GetById(string id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        List<T> GetAll();

    }
}
