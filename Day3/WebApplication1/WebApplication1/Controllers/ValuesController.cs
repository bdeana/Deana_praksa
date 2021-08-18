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


namespace WebApplication1.Controllers
{

    public class Person
    {
        public string firstName, lastName, mail, OIB;
    }
    public class Name
    {
        public static Dictionary<int, string> names = new Dictionary<int, string>();
    }
    public class ValuesController : ApiController
    {
        public SqlConnection connection = new SqlConnection("laptop-oao02np6\\sqlexpress.deana.dbo");



        public List<Person> list_Person = new List<Person>();
        public string Status { get; set; }

        // GET api/values
        [HttpGet]
        [Route("api/Person")]
        public HttpResponseMessage Get(SqlConnection connection)
        {
            using (connection) ;
            SqlCommand command = new SqlCommand("SELECT firstName, lastName FROM Person;", connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            //DataTable schemaTable = reader.GetSchemaTable();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Person person = new Person();
                    person.firstName = reader.GetString(0);
                    person.lastName = reader.GetString(1);

                    list_Person.Add(person);
                }
                connection.Close();
                return Request.CreateResponse(HttpStatusCode.OK, list_Person);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "no data found");
            }
        }

        // GET api/values/5
        [HttpGet]
        [Route("api/Person/{value}")]
        public HttpResponseMessage Get(string value)
        {
            using (connection) ;
            SqlCommand command = new SqlCommand("SELECT firstName FROM Person WHERE OIB = " + value + ";", connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Person person = new Person();
                    person.firstName = reader.GetString(0);
                    //person.lastName = reader.GetString(1);

                    list_Person.Add(person);
                }
                connection.Close();
                return Request.CreateResponse(HttpStatusCode.OK, list_Person);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "no data found");
            }
        }

        // POST api/values

        [HttpPost]
        [Route("api/Person/{OIB}/{mail}")]
        public HttpResponseMessage Post(string OIB, string mail)
        {
            using (connection) ;
            SqlCommand command = new SqlCommand("SELECT firstName FROM Person WHERE OIB = " + OIB + ";", connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                SqlCommand command2 = new SqlCommand("UPDATE Person SET mail = " + mail + "WHERE OIB =" + OIB + ";");
                SqlDataReader reader2 = command2.ExecuteReader();
                connection.Close();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "no data found");
            }
        }



        // PUT api/values/5
        [HttpPut]
        [Route("api/Person/{OIB}/{firstName}/{lastName}/{mail}")]

        public HttpResponseMessage Put(string OIB, string firstName, string lastName, string mail)
        { // stavi samo ako je prazno mjesto
            using (connection) ;
            SqlCommand command = new SqlCommand("SELECT * FROM Person WHERE OIB = " + OIB + ";", connection);
            SqlCommand command2 = new SqlCommand("INSERT INTO Person VALUES (" + OIB + "," + firstName + "," + lastName + "," + mail + ";");
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "OIB already exists.");

            }
            else
            {
                SqlDataReader adapter = command2.ExecuteReader();
                return Request.CreateResponse(HttpStatusCode.OK, "Row inserted.");
            }
            connection.Close();


        }

        // DELETE api/values/5
        [HttpDelete]
        [Route("api/Person/{OIB}")]
        public HttpResponseMessage Delete(string OIB)
        {
            using (connection) ;
            SqlCommand command = new SqlCommand("SELECT firstName FROM Person WHERE OIB = " + OIB + ";", connection);
            SqlCommand command2 = new SqlCommand("DELETE FROM Person WHERE OIB =" + OIB + ";");
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                SqlDataReader reader2 = command2.ExecuteReader();
                connection.Close();
                return Request.CreateResponse(HttpStatusCode.OK, "Row has been deleted.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "no data found");
            }
            connection.Close();

        }
    }
}

