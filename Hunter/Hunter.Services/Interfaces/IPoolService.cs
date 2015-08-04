using System.Collections.Generic;
using Hunter.Services.DtoModels.Models;

namespace Hunter.Services.Interfaces
{
    public interface IPoolService
    {
        IEnumerable<PoolViewModel> GetAllPools();
        PoolViewModel GetPoolById(int id);
        PoolViewModel CreatePool(PoolViewModel pool);
        void UpdatePool(PoolViewModel pool);
        void DeletePool(int id);
        bool IsPoolNameExist(string name);
        bool IsPoolExist(int id);
    }
}
