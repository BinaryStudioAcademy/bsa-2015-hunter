using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Hunter.DataAccess.Db.Base;
using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Interface;
using Hunter.DataAccess.Interface.Base;

namespace Hunter.DataAccess.Db
{
    public class TestRepository : Repository<Test>, ITestRepository
    {
        public TestRepository(DatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }

        public IEnumerable<Test> All(Expression<Func<Test, bool>> condition)
        {
            return QueryIncluding(x => x.File).Where(condition);
        }
    }
}
