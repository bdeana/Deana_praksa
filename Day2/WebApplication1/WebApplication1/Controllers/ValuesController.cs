using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebApplication1.Controllers
{

    public class Name
    {
        public static Dictionary<int, string> names = new Dictionary<int, string>();
    }
    public class ValuesController : ApiController
    {
        public string Status { get; set; }
        
        // GET api/values
        [HttpGet]
        [Route("api/Name")]
        public HttpResponseMessage Get()
        {
            if (Name.names.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "-");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, Name.names);
            }
        }
       
        // GET api/values/5
        [HttpGet]
        [Route("api/Name/{id}")]
        public HttpResponseMessage Get(int id)
        {
            if (Name.names.Count < 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No matching id");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, Name.names[id]);
            }
        }

        // POST api/values
        
        [HttpPost]
        [Route("api/Name/{id}/{value}")]
        public HttpResponseMessage Post(int id, string value)
        {
            if (Name.names.Count < id)
            {
                Name.names.Add(id, value);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Id already exists");
            }
        }

        // PUT api/values/5
        [HttpPut]
        [Route("api/Name/{id}/{value}")]
        
        public HttpResponseMessage Put(int id, string value)
        { // stavi samo ako je prazno mjesto
            if (Name.names.Count < id)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No matching id");
            }
            else
            {
                Name.names[id] = value;
                return Request.CreateResponse(HttpStatusCode.OK);   
            }
        }

        // DELETE api/values/5
        [HttpDelete]
        [Route("api/Name/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            if (Name.names.Count < 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No matching id");
            }
            else
            {
                Name.names.Remove(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }
    }
}
