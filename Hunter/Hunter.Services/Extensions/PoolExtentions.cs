using System.Collections.Generic;
using System.Linq;
using Hunter.DataAccess.Db;

namespace Hunter.Services
{
    public static class PoolExtentions
    {
        public static PoolViewModel ToPoolViewModel(this Pool pool)
        {
            return new PoolViewModel()
            {
                Id = pool.Id,
                Name = pool.Name
            };
        }

        public static IEnumerable<PoolViewModel> ToPoolViewModel(this IEnumerable<Pool> pools)
        {
            return pools.Select(p => new PoolViewModel()
            {
                Id = p.Id, Name = p.Name
            }).ToList();
        }

        public static Pool ToPoolModel(this PoolViewModel poolView)
        {
            return new Pool()
            {
                Id = poolView.Id,
                Name = poolView.Name,
                Vacancy = new List<Vacancy>(),
                Candidate = new List<Candidate>()
            };
        }
    }
}