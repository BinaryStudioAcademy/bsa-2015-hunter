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
    public class CandidateRepository : Repository<Candidate>, ICandidateRepository
    {
        public CandidateRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }
    }
}
