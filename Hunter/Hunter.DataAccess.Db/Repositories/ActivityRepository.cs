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
    public class ActivityRepository : Repository<Activity>, IActivityRepository
    {
        public ActivityRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

    }
}
