using Hunter.Services;
using Hunter.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Hunter.Services.Interfaces;

namespace Hunter.Rest.Controllers
{
    [Authorize]
    [RoutePrefix("api/card")]
    public class CardController : ApiController
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        // GET: api/Card
        [HttpGet]
        [Route("")]
        public IEnumerable<CardDto> Get()
        {
            return new List<CardDto>() { new CardDto() { Id = 1 }, new CardDto(){Id = 2} };
        }

        // GET: api/Card/5
        [HttpGet]
        [Route("")]
        public string Get(int id)
        {
            return "value";
        }


        // POST: api/Card
        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post([FromBody] IEnumerable<CardDto> cards)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _cardService.AddCards(cards, User.Identity.Name);
                    return Request.CreateResponse(HttpStatusCode.OK, "Ok");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid model state");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // PUT: api/Card/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Card/5
        public void Delete(int id)
        {
        }
    }
}
