﻿using Hunter.DataAccess.Db.Base;
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
    }
}
