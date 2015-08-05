using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.DataAccess.Db
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private DbContext dataContext;
        public DbContext Get()
        {
            return dataContext ?? (dataContext = new HunterDbContext());
        }
        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
