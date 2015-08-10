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
        private IDatabaseFactory _databaseFactory;

        protected Repository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
            _dataContext = _databaseFactory.Get();
            _dataSet = _dataContext.Set<T>();
        }

        protected DbContext DataContext
        {
            get { return _dataContext; }
        }


        public IQueryable<T> Query()
        {
            return _dataSet;
        }

        public IEnumerable<T> All()
        {
            return _dataSet.ToList();
        }

        public T Get(long id)
        {
            return _dataSet.FirstOrDefault(x => x.Id == id);
        }

        public T Get(Func<T, Boolean> predicate)
        {
            return _dataContext.Set<T>().FirstOrDefault(predicate);
        }

        public void Delete(T entity)
        {
            var softDelete = entity as ISoftDeleteEntity;
            if (softDelete != null)
            {
                softDelete.IsDeleted = true;
                Update(entity);
            }
            else
            {
                _dataContext.Set<T>().Remove(entity);
            }
        }

        public void DeleteAndCommit(T entity)
        {
            Delete(entity);
            _dataContext.SaveChanges();
        }

        //public void SaveChanges()
        //{
        //    _dataContext.SaveChanges();
        //}

        public void Update(T entity)
        {
            BeforeSave(entity);
            if (_dataContext.Entry(entity).State == EntityState.Detached)
            {
                _dataContext.Set<T>().Attach(entity);
                _dataContext.Entry(entity).State = entity.IsNew() ? EntityState.Added: EntityState.Modified;
            }
        }

        public void UpdateAndCommit(T entity)
        {
            Update(entity);
            _dataContext.SaveChanges();
        }


        /// <summary>
        /// Method is executed before calling Save and SaveAndCommit.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void BeforeSave(T entity)
        {
        }
    }
}