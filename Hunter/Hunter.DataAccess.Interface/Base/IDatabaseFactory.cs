using System;
using System.Data.Entity;

namespace Hunter.DataAccess.Interface.Base
{
    public interface IDatabaseFactory : IDisposable
    {
        DbContext Get();
    }
}
