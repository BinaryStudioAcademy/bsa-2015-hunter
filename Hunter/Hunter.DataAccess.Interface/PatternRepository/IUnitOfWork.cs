using Hunter.DataAccess.Db;
using System;

namespace Hunter.DataAccess.Interface
{
    public interface IUnitOfWork //: IDisposable
    {
        //IRepository<T> Repository<T>() where T : class, IEntity;
        void SaveChanges();
    }
}