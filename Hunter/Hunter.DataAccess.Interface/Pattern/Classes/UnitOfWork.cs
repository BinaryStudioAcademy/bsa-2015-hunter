﻿using System;
using Hunter.DataAccess.Db;
using System.Data.Entity;

namespace Hunter.DataAccess.Interface
{
    public class UnitOfWork : IUnitOfWork
    {
       // private readonly HunterDbContext _dataContext = new HunterDbContext();

        private DbContext _dataContext;
        private readonly IDatabaseFactory dbFactory;

        public UnitOfWork(IDatabaseFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        protected DbContext DbContext
        {
            get
            {
                return _dataContext ?? dbFactory.Get();
            }
        }
        //public IRepository<T> Repository<T>() where T : class, IEntity
        //{
        //    return new Repository<T>(_dataContext);
        //}

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }

        //public void Dispose()
        //{
        //    _dataContext.Dispose();
        //}
    }
}