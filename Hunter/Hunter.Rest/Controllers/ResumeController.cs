using System;
using System.Web.Http;
using System.Web.Http.Description;
using Hunter.Services.Services;
using Hunter.Tools.LinkedIn;

namespace Hunter.Rest.Controllers
{
    [RoutePrefix("api/Resume")]
    public class ResumeController : ApiController
    {
        private readonly IResumeService _resumeService;

        public ResumeController(IResumeService resumeService)
        {
            _resumeService = resumeService;
        }


        [Route("parse")]
        [ResponseType(typeof(PublicPageInfo))]
        public IHttpActionResult GetLinkedInInfo([FromUri]string url)
        {
           // url = "https://ua.linkedin.com/in/ihorsyerkov";

            try
            {
                var profile = _resumeService.GetLikenIdInfo(url);
                return Ok(profile);
            }
            catch (Exception)
            {
                return BadRequest("Wrong LinkedIn Url");
            }
        }
    }
}
