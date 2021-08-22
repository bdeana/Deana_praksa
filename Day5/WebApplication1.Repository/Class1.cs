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

        public async Task<List<Person>> Get() // SqlConnection connection)
        {
            //SqlConnection connection = new SqlConnection("Data Source = laptop-oao02np6\\sqlexpress;Initial Catalog = deana; Integrated Security = True");
            using (connection)
            {
                List<Person> list_Person = new List<Person>();
                SqlCommand command = new SqlCommand("SELECT * FROM Person;", connection);
                await connection.OpenAsync();

                SqlDataReader reader = command.ExecuteReaderAsync();
                //DataTable schemaTable = reader.GetSchemaTable();
                if (reader.HasRows)
                {
                    while (reader.ReadAsync())
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

        public async Task<Person> Get(string value)
        {
            using (connection)
            {

                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT firstName FROM Person WHERE OIB = " + value + ";", connection);

                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    Person person = new Person();
                    while (reader.ReadAsync())
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


        public async Task<bool> Post(string OIB, string mail)
        {
            bool a = false;
            using (connection)
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT * FROM Person;", connection);
                SqlCommand command2 = new SqlCommand("UPDATE Person SET mail =" + mail + " WHERE OIB = " + OIB + ";", connection);

                command.Parameters.Add("@OIB", SqlDbType.VarChar).Value = OIB;
                command2.Parameters.Add("@mail", SqlDbType.VarChar).Value = mail;

                SqlDataReader reader = command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    a = true;
                }
                reader.Close();
                if (a)
                {
                    command2.ExecuteNonQueryAsync();
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



        public async Task<bool> Put(string OIB, string firstName, string lastName, string mail)
        { // stavi samo ako je prazno mjesto
            Person person = new Person();
            person.firstName = firstName;
            person.lastName = lastName;
            person.OIB = OIB;
            person.mail = mail;
            bool a = false;

            using (connection)
            {

                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT * FROM Person WHERE OIB = " + OIB + ";", connection);
                SqlCommand command2 = new SqlCommand("INSERT INTO Person (OIB, firstName, lastName, mail) VALUES (" + OIB + "," + firstName + "," + lastName + "," + mail + ";");
                command.Parameters.Add("OIB", SqlDbType.VarChar).Value = person.firstName;

                SqlDataReader reader = command.ExecuteReaderAsync();
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
                    await command2.ExecuteNonQueryAsync();
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


        public async Task<bool> Delete(string OIB)
        {
            using (connection)
            {

                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT firstName FROM Person WHERE OIB = " + OIB + ";", connection);
                SqlCommand command2 = new SqlCommand();
                SqlCommand command3 = new SqlCommand();

                command.Parameters.Add("@OIB", SqlDbType.VarChar).Value = OIB;


                SqlDataReader reader = await command.ExecuteReaderAsync();
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
                    await command3.ExecuteNonQueryAsync();
                    await command2.ExecuteNonQueryAsync();
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
