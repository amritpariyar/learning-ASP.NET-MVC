using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace SOIT.Controllers
{
    public class ExchangeController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetOrganizationInformation()
        {
            var detail = new
            {
                Name = "Civil",
                Address = "Kathmandu"
            };

            return Request.CreateResponse(HttpStatusCode.OK, detail, new JsonMediaTypeFormatter(), "application/json");
        }
    }
}
