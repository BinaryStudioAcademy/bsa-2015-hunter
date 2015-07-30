using Hunter.DataAccess.Db;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Hunter.DataAccess.Interface
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly DbContext _dataContext;
        protected readonly DbSet<T> _dataSet;

        public Repository(DbContext dataContext)
        {
            _dataContext = dataContext;
            _dataSet = dataContext.Set<T>();
        }

        public IQueryable<T> Query()
        {
            return _dataSet;
        }

        public IEnumerable<T> All()
        {
            return _dataSet.ToList();
        }

        public T Get(Int32 id)
        {
            return _dataSet.FirstOrDefault(x => x.Id == id);
        }

        public T Get(Func<T, Boolean> predicate)
        {
            return _dataSet.FirstOrDefault(predicate);
        }

        public void Add(T entity)
        {
            _dataSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dataSet.Remove(entity);
        }

        public void SaveChanges()
        {
            _dataContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _dataSet.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
        }
    }
}