using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Text.RegularExpressions;
using WebApplication1.Model;
using WebApplication1.Repository;
using WebApplication1.Service;
using WebApplication1.WebApi;
using WebApplication1.Model.Common;
using WebApplication1.Repository.Common;
using WebApplication1.Service.Common;

namespace WebApplication1.Controllers
{

    public class Person
    {
        public string firstName, lastName, mail, OIB;
    }
    public class Dog
    {
        public int CIP, bir_year;
        public string name, person_id;
    }
    
    public class ValuesController : ApiController
    {
        //SqlConnection connection = new SqlConnection("Data Source = laptop-oao02np6\\sqlexpress;Initial Catalog = deana; Integrated Security = True");



        //public List<Person> list_Person = new List<Person>();


        IService iService;
        public ValuesController(IService _IService)
        {
            iService = _IService;
        }

        // GET api/values
        [HttpGet] 
        [Route("api/Person")]
        public async Task<HttpResponseMessage> Get() // SqlConnection connection)
        {
            //SqlConnection connection = new SqlConnection("Data Source = laptop-oao02np6\\sqlexpress;Initial Catalog = deana; Integrated Security = True");
            List<Person> list_Person = await iService.Get();

            if(list_Person == null)
                { 
                   
                    return Request.CreateResponse(HttpStatusCode.NotFound, "no data found");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, list_Person);
                }
            
        }

        // GET api/values/5
        [HttpGet]
        [Route("api/Person/{value}")]
        public async Task<HttpResponseMessage> Get(string value)
        {

            Person person = await iService.Get(value);
         
                if (person == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "no data found");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, person);
                }
            
        }
        //_________________________________________________________________

        // POST api/values

        [HttpPost]
        [Route("api/Person/{OIB}/{mail}")]
        public async Task<HttpResponseMessage> Post(string OIB, string mail)
        {
            bool feedback = await iService.Post(OIB, mail);
            if (feedback != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "no data found");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Row has been updated.");
            }

        }



        // PUT api/values/5
        [HttpPut]
        [Route("api/Person/{OIB}/{firstName}/{lastName}/{mail}")]

        public async Task<HttpResponseMessage> Put(string OIB, string firstName, string lastName, string mail)
        {
            bool feedback = await iService.Put(OIB, firstName, lastName, mail);

            if (feedback != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Person already exists.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Row inserted.");
            }

        }

        // DELETE api/values/5
        [HttpDelete]
        [Route("api/Person/{OIB}")]
        public async Task<HttpResponseMessage> Delete(string OIB)
        {
            bool feedback = await iService.Delete(OIB);
            if (feedback != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "no data found");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Row has been deleted.");
            }

        }
    }
}

