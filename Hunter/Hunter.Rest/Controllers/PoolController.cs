using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Hunter.DataAccess.Db;
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
        public IEnumerable<Pool> Get()
        {
            return _poolService.GetAllPools();
        }

        // GET: api/Pool/5
        public Pool Get(int id)
        {
            var pool = _poolService.GetPoolById(id);

            if (pool == null)
            {
                return new Pool();
            }
            return pool;
        }

        // POST: api/Pool
        public void Post(Pool pool)
        {
            _poolService.CreatePool(pool);
        }

        // PUT: api/Pool/5
        public void Put(Pool pool)
        {
            _poolService.UpdatePool(pool);
        }

        // DELETE: api/Pool/5
        public void Delete(Pool pool)
        {
            _poolService.DeletePool(pool);
        }
    }
}
