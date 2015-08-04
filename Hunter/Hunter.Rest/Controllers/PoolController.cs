using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Hunter.DataAccess.Db;
using Hunter.Rest.DtoModels.Extentions;
using Hunter.Rest.DtoModels.Models;
using Hunter.Services.Interfaces;


namespace Hunter.Rest.Controllers
{
    public class PoolController : ApiController
    {
        private readonly IPoolService _poolService;

        public PoolController(IPoolService poolService)
        {
            _poolService = poolService;
        }

        // GET: api/Pool
        public IEnumerable<PoolViewModel> Get()
        {
            var pools = _poolService.GetAllPools();

            if (pools == null)
            {
                return null;
            }

            return pools.ToPoolViewModel();
        }

        // GET: api/Pool/5
        public PoolViewModel Get(int id)
        {
            var pool = _poolService.GetPoolById(id);

            if (pool == null)
            {
                return null;
            }
            return pool.ToPoolViewModel();
        }

        // POST: api/Pool
        public void Post(PoolViewModel poolView)
        {
            _poolService.CreatePool(poolView.ToPoolModel());
        }

        // PUT: api/Pool/5
        public void Put(PoolViewModel poolView)
        {
            _poolService.UpdatePool(poolView.ToPoolModel());
        }

        // DELETE: api/Pool/5
        public void Delete(PoolViewModel poolView)
        {
            _poolService.DeletePool(poolView.ToPoolModel());
        }

        //public bool IsPoolExists(string name)
        //{
        //    return _poolService.IsPoolExists(name);
        //}
    }
}
