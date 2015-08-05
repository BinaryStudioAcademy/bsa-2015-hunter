using Hunter.DataAccess.Interface;
using Hunter.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.DataAccess.Db
{
    public class ResumeRepository : Repository<Resume>, IResumeRepository
    {
        public ResumeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }
    }
}
