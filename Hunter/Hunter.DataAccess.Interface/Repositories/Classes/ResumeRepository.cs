using Hunter.DataAccess.Db;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.DataAccess.Interface.Repositories.Classes
{
    public class ResumeRepository : Repository<Resume>, IResumeRepository
    {
        public ResumeRepository(DbContext dataContext)
            : base(dataContext)
        { }
    }
}
