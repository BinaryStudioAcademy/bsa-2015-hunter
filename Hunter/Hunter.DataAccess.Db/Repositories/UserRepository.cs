using Hunter.DataAccess.Db.Base;
using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Interface.Base;
using Hunter.DataAccess.Interface.Repositories;

namespace Hunter.DataAccess.Db
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }
    }
}
