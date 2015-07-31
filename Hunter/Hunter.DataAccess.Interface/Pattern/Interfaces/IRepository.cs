using System;
using System.Collections.Generic;
using System.Linq;
using Hunter.DataAccess.Db;

namespace Hunter.DataAccess.Interface
{
    public interface IRepository<T> where T : class//, IEntity
    {
        IQueryable<T> Query();
        IEnumerable<T> All();
        T Get(Int32 id);
        T Get(Func<T, bool> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        //void SaveChanges();
    }
}