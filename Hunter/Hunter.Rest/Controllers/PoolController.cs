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

        //private PoolService _poolService = new PoolService(new PoolRepository(), );

        // GET: api/Pool
        public IEnumerable<Pool> Get()
        {
            
            //return new string[] { "value1", "value2" };
            var pools = new List<Pool>
            {
                new Pool(){Id = 1, Name = "JS"},
                new Pool(){Id = 2, Name = ".Net"}
            };
            return pools;
        }

        // GET: api/Pool/5
        public Pool Get(int id)
        //public string Get(int id)
        {
            //var pool = _poolService.GetPoolById(id);

            //if (pool == null)
            //{
            //    return new Pool();
            //}
            //return pool;
            //    return "value";
            return new Pool(){Id = 1, Name = "JS"};
        }

        // POST: api/Pool
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Pool/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Pool/5
        public void Delete(int id)
        {
        }
    }
}
