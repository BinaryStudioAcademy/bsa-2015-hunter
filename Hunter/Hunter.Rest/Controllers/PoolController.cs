using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Hunter.DataAccess.Db;
using Hunter.DataAccess.Interface.Repositories.Classes;
using Hunter.Services;
using Hunter.Services.Interfaces;
using Hunter.Services.Concrete;

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
        public IEnumerable<Pool> Get()
        {
            //return new List<Pool>[] { "value1", "value2" };
            return _poolService.GetAllPools();
        }

        // GET: api/Pool/5
        public Pool Get(int id)
        //public string Get(int id)
        {
            var pool = _poolService.GetPoolById(id);

            if (pool == null)
            {
                return new Pool();
            }
            return pool;
            //return "value";
            
        }

        // POST: api/Pool
        public void Post(Pool pool)
        {
            _poolService.CreatePool(pool);
        }

        // PUT: api/Pool/5
        public void Put(Pool pool)
        {
            //Pool pool = _poolService.GetPoolById(id);

            _poolService.UpdatePool(pool);
        }

        // DELETE: api/Pool/5
        public void Delete(Pool pool)
        {
            //Pool pool = _poolService.GetPoolById(id);

            _poolService.DeletePool(pool);
        }
    }
}
