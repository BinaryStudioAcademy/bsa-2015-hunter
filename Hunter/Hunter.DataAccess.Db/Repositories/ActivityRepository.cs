using System.Collections.Generic;
using System.Linq;
using Hunter.DataAccess.Db.Base;
using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Interface;
using Hunter.DataAccess.Interface.Base;

namespace Hunter.DataAccess.Db
{
    public class ActivityRepository : Repository<Activity>, IActivityRepository
    {
        public ActivityRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        public int ActualActivityAmount(int lastViewd)
        {
            var res = DataContext.Set<Activity>().Count(x => x.Id > lastViewd);

            return res;
        }

        public IEnumerable<Activity> All()
        {
            return base.All().OrderByDescending(x => x.Time);
        }
    }
}
