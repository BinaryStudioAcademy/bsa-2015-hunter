using Hunter.DataAccess.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Services.Interfaces
{
    public interface IPoolService
    {
        IEnumerable<Pool> GetAllPools();
        Pool GetPoolById(int id);
        void CreatePool(Pool pool);
        void UpdatePool(Pool pool);
        void DeletePool(Pool pool);
        bool IsPoolExists(string name);
    }
}
