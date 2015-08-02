using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.DataAccess.Db;
using Hunter.DataAccess.Interface;
using Hunter.Services.Interfaces;

namespace Hunter.Services.Concrete
{
    public class PoolService : IPoolService
    {
        private readonly IPoolRepository _poolRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public PoolService(IPoolRepository poolRepository, IUnitOfWork unitOfWork)
        {
            _poolRepository = poolRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Pool> GetAllPools()
        {
            try
            {
                
                //var pools = new List<Pool>
                //{
                //    new Pool(){Id = 1, Name = "JS"},
                //    new Pool(){Id = 2, Name = ".Net"}
                //};
                //return pools;

                return _poolRepository.All();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return new Pool[0];
            }
        }

        public Pool GetPoolById(int id)
        {
            try
            {
                return _poolRepository.Get(id);

                //return new Pool { Id = 1, Name = "test" };
            }
            catch (Exception ex)
            {
                return new Pool();
            }
        }

        public void CreatePool(Pool pool)
        {
            try
            {
                _poolRepository.Add(pool);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {

            }

        }

        public void UpdatePool(Pool pool)
        {
            try
            {
                _poolRepository.Update(pool);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public void DeletePool(Pool pool)
        {
            try
            {
                _poolRepository.Delete(pool);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
