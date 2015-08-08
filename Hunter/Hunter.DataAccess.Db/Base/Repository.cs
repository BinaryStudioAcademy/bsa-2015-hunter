using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Interface.Base;

namespace Hunter.DataAccess.Db.Base
{
    public abstract class Repository<T> : IRepository<T> where T : class, IEntity,new()
    {
        private  DbContext _dataContext;
        private readonly DbSet<T> _dataSet;
        protected Repository(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            _dataSet = DataContext.Set<T>();
        }

        //public Repository(DbContext dataContext)
        //{
        //    _dataContext = dataContext;
        //    _dataSet = dataContext.Set<T>();
        //}


        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        protected DbContext DataContext
        {
            get { return _dataContext ?? (_dataContext = DatabaseFactory.Get()); }
        }


        public IQueryable<T> Query()
        {
            return _dataSet.AsQueryable();
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

        public T Add(T entity)
        {
            entity = _dataSet.Add(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _dataSet.Remove(entity);
        }

        //public void SaveChanges()
        //{
        //    _dataContext.SaveChanges();
        //}

        public void Update(T entity)
        {
            _dataSet.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
        }
    }
}