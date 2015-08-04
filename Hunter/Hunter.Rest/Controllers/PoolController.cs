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
        [System.Web.Mvc.HttpGet]
        public IEnumerable<PoolViewModel> Get()
        {
            var pools = _poolService.GetAllPools();

            return pools == null ? null : pools.ToPoolViewModel();
        }

        // GET: api/Pool/5
        [System.Web.Mvc.HttpGet]
        public PoolViewModel Get(int id)
        {
            var pool = _poolService.GetPoolById(id);

            return pool == null ? null : pool.ToPoolViewModel();
        }

        // POST: api/Pool
        [System.Web.Mvc.HttpPost]
        public void Post(PoolViewModel poolView)
        {
            _poolService.CreatePool(poolView.ToPoolModel());
        }

        // PUT: api/Pool/5
        [System.Web.Mvc.HttpPut]
        public void Put(PoolViewModel poolView)
        {
            _poolService.UpdatePool(poolView.ToPoolModel());
        }

        // DELETE: api/Pool/5
        [System.Web.Mvc.HttpDelete]
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
