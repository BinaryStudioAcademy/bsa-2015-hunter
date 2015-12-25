using Hunter.DataAccess.Db.Base;
using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Entities.Entites;
using Hunter.DataAccess.Interface;
using Hunter.DataAccess.Interface.Base;

namespace Hunter.DataAccess.Db
{
    public class RoleMappingRepository : Repository<RoleMapping>, IRoleMappingRepository
    {
        public RoleMappingRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }
    }
}
