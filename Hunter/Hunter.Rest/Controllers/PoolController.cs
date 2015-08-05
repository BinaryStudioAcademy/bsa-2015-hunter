using System.Collections.Generic;
using System.Web.Http;
using Hunter.Services.Dto;
using Hunter.Services.Services.Interfaces;

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
            return _poolService.GetAllPools();
        }

        // GET: api/Pool/5
        [System.Web.Mvc.HttpGet]
        public PoolViewModel Get(int id)
        {
            return _poolService.GetPoolById(id);
        }

        // POST: api/Pool
        [System.Web.Mvc.HttpPost]
        public IHttpActionResult Post([FromBody] PoolViewModel poolViewModel)
        {
            if (_poolService.IsPoolNameExist(poolViewModel.Name))
            {
                return BadRequest("Pool name alreafy exists!");
            }

            poolViewModel = _poolService.CreatePool(poolViewModel);

            if (poolViewModel != null)
            {
                return Created(Request.RequestUri + poolViewModel.Id.ToString(), poolViewModel);
            }

            return InternalServerError();
        }

        // PUT: api/Pool/5
        [System.Web.Mvc.HttpPut]
        public IHttpActionResult Put(int id, [FromBody] PoolViewModel poolViewModel)
        {
            if (!_poolService.IsPoolExist(id))
            {
                return NotFound();
            }
            
            poolViewModel.Id = id;
            _poolService.UpdatePool(poolViewModel);
            
            return Ok(poolViewModel);
        }

        // DELETE: api/Pool/5
        [System.Web.Mvc.HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (!_poolService.IsPoolExist(id))
            {
                return NotFound();
            }

            _poolService.DeletePool(id);

            return Ok(id);
        }
    }
}
