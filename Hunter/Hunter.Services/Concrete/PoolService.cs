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
        private readonly Common.Interfaces.ILogger _logger;

        public PoolService(IPoolRepository poolRepository, IUnitOfWork unitOfWork, Common.Interfaces.ILogger logger)
        {
            _poolRepository = poolRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IEnumerable<Pool> GetAllPools()
        {
            try
            {
                return _poolRepository.All();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new Pool[0];
            }
        }

        public Pool GetPoolById(int id)
        {
            try
            {
                return _poolRepository.Get(id);

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
                _logger.Log(ex);
            }

        }

        public void UpdatePool(Pool pool)
        {
            try
            {
                //var updatePool = _poolRepository.Get(pool.Id);

                _poolRepository.Update(pool);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
        }

        public void DeletePool(Pool pool)
        {
            try
            {
                var deletePool = _poolRepository.Get(pool.Id);
                _poolRepository.Delete(deletePool);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
        }
    }
}
