using System;
using Hunter.DataAccess.Db;

namespace Hunter.DataAccess.Interface
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HunterDbContext _dataContext = new HunterDbContext();

        public IRepository<T> Repository<T>() where T : class, IEntity
        {
            return new Repository<T>(_dataContext);
        }

        public void SaveChanges()
        {
            _dataContext.SaveChanges();
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}