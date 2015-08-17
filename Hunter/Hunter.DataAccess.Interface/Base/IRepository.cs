using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Hunter.DataAccess.Interface.Base
{
    public interface IRepository<T> where T : class//, IEntity
    {
        IQueryable<T> Query();
        IQueryable<T> QueryIncluding(params Expression<Func<T, object>>[] includes);
        IEnumerable<T> All();
        T Get(long id);
        T Get(Func<T, bool> predicate);
        //T Add(T entity);
        void Update(T entity);
        void UpdateAndCommit(T entity);

        void Delete(T entity);
        void DeleteAndCommit(T entity);
        //void SaveChanges();
    }
}