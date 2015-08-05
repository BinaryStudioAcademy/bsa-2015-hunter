using System;
using System.Collections.Generic;
using System.Linq;
using Hunter.Common.Interfaces;
using Hunter.DataAccess.Interface;
using Hunter.Services.Dto;
using Hunter.Services.Extentions;
using Hunter.Services.Services.Interfaces;

namespace Hunter.Services.Services
{
    public class PoolService : IPoolService
    {
        private readonly IPoolRepository _poolRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public PoolService(IPoolRepository poolRepository, IUnitOfWork unitOfWork, ILogger logger)
        {
            _poolRepository = poolRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IEnumerable<PoolViewModel> GetAllPools()
        {
            try
            {
                return _poolRepository.All().ToPoolViewModel();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new PoolViewModel[0];
            }
        }

        public PoolViewModel GetPoolById(int id)
        {
            try
            {
                return _poolRepository.Get(id).ToPoolViewModel();

            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new PoolViewModel();
            }
        }

        public PoolViewModel CreatePool(PoolViewModel poolViewModel)
        {
            try
            {
                var pool = _poolRepository.Add(poolViewModel.ToPoolModel());
                _unitOfWork.SaveChanges();
                return pool.ToPoolViewModel();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return null;
            }
        }

        public void UpdatePool(PoolViewModel poolViewModel)
        {
            try
            {
                _poolRepository.Update(poolViewModel.ToPoolModel());
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
        }

        public void DeletePool(int id)
        {
            try
            {
                _poolRepository.Delete(_poolRepository.Get(id));
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
        }

        public bool IsPoolNameExist(string name)
        {
            try
            {
                return _poolRepository.All().Any(p => string.Equals(p.Name, name, StringComparison.CurrentCultureIgnoreCase));
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return false;
            }
        }

        public bool IsPoolExist(int id)
        {
            try
            {
                return _poolRepository.All().Any(p => p.Id == id);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return false;
            }
        }
    }
}
