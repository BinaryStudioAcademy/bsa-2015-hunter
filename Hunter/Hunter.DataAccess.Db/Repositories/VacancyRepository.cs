using Hunter.DataAccess.Db.Base;
using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Interface;
using Hunter.DataAccess.Interface.Base;

namespace Hunter.DataAccess.Db
{
    public class VacancyRepository : Repository<Vacancy>, IVacancyRepository
    {
        public VacancyRepository(DatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }
    }
}
