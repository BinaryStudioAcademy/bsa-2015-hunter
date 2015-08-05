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
    public class InterviewRepository : Repository<Interview>, IInterviewRepository
    {
        public InterviewRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }
    }
}
