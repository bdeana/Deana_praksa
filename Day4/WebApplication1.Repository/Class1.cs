using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Model;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace WebApplication1.Repository
{
    public class Class1R
    {

        SqlConnection connection = new SqlConnection("Data Source = laptop-oao02np6\\sqlexpress;Initial Catalog = deana; Integrated Security = True");

        public List<Person> Get() // SqlConnection connection)
        {
            //SqlConnection connection = new SqlConnection("Data Source = laptop-oao02np6\\sqlexpress;Initial Catalog = deana; Integrated Security = True");
            using (connection)
            {
                List<Person> list_Person = new List<Person>();
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
                    reader.Close();
                    connection.Close();
                    return list_Person;
                }
                else
                {
                    reader.Close();
                    connection.Close();
                    return null;
                }
            }
        }

        public Person Get(string value)
        {
            using (connection)
            {

                SqlCommand command = new SqlCommand("SELECT firstName FROM Person WHERE OIB = " + value + ";", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    Person person = new Person();
                    while (reader.Read())
                    {
                        //Person person = new Person();
                        person.firstName = reader.GetString(0);
                        //person.lastName = reader.GetString(1);

                        
                    }
                    reader.Close();
                    connection.Close();
                    return person;
                }
                else
                {
                    reader.Close();
                    connection.Close();
                    return null;
                }
            }
        }


        public bool Post(string OIB, string mail)
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
                    return true;

                }
                else
                {
                    connection.Close();
                    return false;
                }


            }
        }



        public bool Put(string OIB, string firstName, string lastName, string mail)
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
                    return true;
                }
                else
                {
                    reader.Close();
                    connection.Close();
                    return false;
                }
            }


        }


        public bool Delete(string OIB)
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
                    return true;
                }
                else
                {
                    connection.Close();
                    return false;
                }
            }

        }










    }
}
