using Hunter.DataAccess.Db;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.DataAccess.Interface.Repositories.Classes
{
    public class CandidateRepository : Repository<Candidate>, ICandidateRepository
    {
        public CandidateRepository(DbContext dataContext)
            : base(dataContext)
        { }
    }
}
