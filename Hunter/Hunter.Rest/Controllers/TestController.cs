using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Hunter.Services.Dto;
using Hunter.Services.Services.Interfaces;

namespace Hunter.Rest.Controllers
{
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetTest(int cardId)
        {
            try
            {
                var test = _testService.GetCandidateTest(cardId);
                return Ok(test);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetAllTests(int candidateId)
        {
            try
            {
                var tests = _testService.GetAllCandidatesTests(candidateId);
                return Ok(tests);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult AddTest(TestDto test)
        {
            try
            {
                _testService.AddTest(test);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateTest(TestDto test)
        {
            try
            {
                _testService.UpdateTest(test);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteTest([FromBody]int testId)
        {
            try
            {
                _testService.DeleteTestById(testId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
