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
    public class Dog
    {
        public int CIP, bir_year;
        public string name, person_id;
    }
    public class Name
    {
        public static Dictionary<int, string> names = new Dictionary<int, string>();
    }
    public class ValuesController : ApiController
    {
        SqlConnection connection = new SqlConnection("Data Source = laptop-oao02np6\\sqlexpress;Initial Catalog = deana; Integrated Security = True");



        public List<Person> list_Person = new List<Person>();
        //private object sqlDbType;

        public string Status { get; set; }

        // GET api/values
        [HttpGet] 
        [Route("api/Person")]
        public HttpResponseMessage Get() // SqlConnection connection)
        {
            //SqlConnection connection = new SqlConnection("Data Source = laptop-oao02np6\\sqlexpress;Initial Catalog = deana; Integrated Security = True");
            using (connection)
            {

                SqlCommand command = new SqlCommand("SELECT * FROM Person;", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                //DataTable schemaTable = reader.GetSchemaTable();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Person person = new Person();
                        person.OIB = reader.GetString(0);
                        person.firstName = reader.GetString(1);
                        person.lastName = reader.GetString(2);
                        person.mail = reader.GetString(3);

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
        }

        // GET api/values/5
        [HttpGet]
        [Route("api/Person/{value}")]
        public HttpResponseMessage Get(string value)
        {
            using (connection)
            {

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
        }
        //_________________________________________________________________

        // POST api/values

        [HttpPost]
        [Route("api/Person/{OIB}/{mail}")]
        public HttpResponseMessage Post(string OIB, string mail)
        {
            bool a = false;
            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Person;", connection);
                SqlCommand command2 = new SqlCommand("UPDATE Person SET mail =" + mail + " WHERE OIB = " + OIB + ";", connection);
                connection.Open();

                command.Parameters.Add("@OIB", SqlDbType.VarChar).Value = OIB;
                command2.Parameters.Add("@mail", SqlDbType.VarChar).Value = mail;

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    a = true;
                }
                reader.Close();
                if (a)
                {
                    command2.ExecuteNonQuery();
                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, "Row has been updated.");

                }
                else
                {
                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.NotFound, "no data found");
                }


            }
        }



        // PUT api/values/5
        [HttpPut]
        [Route("api/Person/{OIB}/{firstName}/{lastName}/{mail}")]

        public HttpResponseMessage Put(string OIB, string firstName, string lastName, string mail)
        { // stavi samo ako je prazno mjesto
            Person person = new Person();
            person.firstName = firstName;
            person.lastName = lastName;
            person.OIB = OIB;
            person.mail = mail;
            bool a = false;
            
            using (connection)
            {

                SqlCommand command = new SqlCommand("SELECT * FROM Person WHERE OIB = " + OIB + ";", connection);
                SqlCommand command2 = new SqlCommand("INSERT INTO Person (OIB, firstName, lastName, mail) VALUES (" + OIB + "," + firstName + "," + lastName + "," + mail + ";");
                command.Parameters.Add("OIB", SqlDbType.VarChar).Value = person.firstName;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    a = true;
                    //return Request.CreateResponse(HttpStatusCode.OK, "Row inserted.");
                }

                if (a)
                {
         
                    command2.Parameters.Add("@OIB", SqlDbType.VarChar).Value = OIB;
                    command2.Parameters.Add("@firstName", SqlDbType.VarChar).Value = firstName;
                    command2.Parameters.Add("@lastName", SqlDbType.VarChar).Value = lastName;
                    command2.Parameters.Add("@mail", SqlDbType.VarChar).Value = mail;

                    reader.Close();
                    //SqlDataReader adapter = command2.ExecuteReader();
                    command2.ExecuteNonQuery();
                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, "Row inserted.");
                }
                else
                {
                    reader.Close();
                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Person already exists.");
                }
            }


        }

        // DELETE api/values/5
        [HttpDelete]
        [Route("api/Person/{OIB}")]
        public HttpResponseMessage Delete(string OIB)
        {
            using (connection)
            {

                SqlCommand command = new SqlCommand("SELECT firstName FROM Person WHERE OIB = " + OIB + ";", connection);
                SqlCommand command2 = new SqlCommand();
                SqlCommand command3 = new SqlCommand();
                connection.Open();
                
                command.Parameters.Add("@OIB", SqlDbType.VarChar).Value = OIB;


                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    command3 = new SqlCommand("DELETE FROM Dog WHERE person_id = " + OIB + ";", connection);
                    command3.Parameters.Add("@OIB", SqlDbType.VarChar).Value = OIB;


                    while (reader.Read())
                    {
                        command2 = new SqlCommand("DELETE FROM Person WHERE OIB =" + OIB + ";", connection);
                        command2.Parameters.Add("@OIB", SqlDbType.VarChar).Value = OIB;
                    }
                    //SqlDataReader reader2 = command2.ExecuteReader();
                    //SqlDataReader reader2 = command2.DeleteCommand.ExecuteReader();
                    reader.Close();
                    command3.ExecuteNonQuery();
                    command2.ExecuteNonQuery();
                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, "Row has been deleted.");
                }
                else
                {
                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.NotFound, "no data found");
                }
            }

        }
    }
}

