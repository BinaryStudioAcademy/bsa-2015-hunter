using Hunter.DataAccess.Db;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.DataAccess.Interface.Repositories.Classes
{
    public class TestRepository : Repository<Test>, ITestRepository
    {
        public TestRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }
    }
}
