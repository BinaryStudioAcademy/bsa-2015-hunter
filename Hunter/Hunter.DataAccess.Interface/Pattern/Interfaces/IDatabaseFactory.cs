using System;
using System.Data.Entity;

namespace Hunter.DataAccess.Interface
{
    public interface IDatabaseFactory : IDisposable
    {
        DbContext Get();
    }
}
