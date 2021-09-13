using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading.Tasks;
using ISDB.Model;
using ISDBRepository.Common;
using ISDB.Common;

namespace ISDB.Repository
{
    public class UserRepository : IUserRepository


    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-BB9OT7S\\SQLEXPRESS;Initial Catalog=ISDB;Integrated Security=True");


        public async Task<List<UserModel>> GetUserAsync(Sorter sorter=null, Pager pager=null, UserFilter userFilter=null)
        {


            Sorter Sorting = new Sorter();
            string sort = Sorting.SortBy(sorter.Order_by, sorter.Sort_Order);

            Pager Paging = new Pager();
            string page = Paging.Page(pager.Page_number, pager.Page_size);

            UserFilter Filter = new UserFilter();
            string filter = Filter.FilterLike(userFilter.Filter);


            SqlCommand command = new SqlCommand
                ("SELECT * FROM User_ " + filter + sort + page + " ;", connection);
            await connection.OpenAsync();


            SqlDataReader reader = await command.ExecuteReaderAsync();
            List<UserModel> list_of_users = new List<UserModel>();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    UserModel user = new UserModel();
                    user.User_id = reader.GetInt32(0);
                    user.First_name = reader.GetString(1);
                    user.Last_name = reader.GetString(2);
                    user.Email = reader.GetString(3);
                    user.Country = reader.GetString(4);
                  
                    list_of_users.Add(user);
                }
                reader.Close();
                connection.Close();
                return list_of_users;
            }
            else
            {
                reader.Close();
                connection.Close();
                return null;
            }
        }


        public async Task<UserModel> GetUserByIdAsync(int User_id)
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand
                ("SELECT * FROM User_ WHERE User_id = @User_id;", connection);

            SqlParameter id_parameter = new SqlParameter("@User_id", System.Data.SqlDbType.Int);

            command.Parameters.Add(id_parameter).Value = User_id;

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                UserModel user = new UserModel();
                while (await reader.ReadAsync())
                {

                    user.User_id = User_id;
                    user.First_name = reader.GetString(1);
                    user.Last_name = reader.GetString(2);
                    user.Email = reader.GetString(3);
                    user.Country = reader.GetString(4);


                }
                connection.Close();
                return user;
            }
            else
            {
                connection.Close();
                return null;
            }
        }

        public async Task<bool> CreateUserAsync(UserModel user)
        {

            UserModel _user = new UserModel();
            _user.User_id = user.User_id;
            _user.First_name = user.First_name;
            _user.Last_name = user.Last_name;
            _user.Email = user.Email;
            _user.Country = user.Country;
          

            SqlCommand command = new SqlCommand("INSERT INTO User_ (User_id, First_name , Last_name , Email, Country)" +
                                                "VALUES (@User_id, @First_name, @Last_name, @Email, @Country); ", connection);

            SqlCommand command_id = new SqlCommand("SELECT * FROM User_ WHERE User_id = @User_id ", connection);
            command_id.Parameters.Add("User_id", SqlDbType.Int).Value = _user.User_id;


            await connection.OpenAsync();

            SqlDataReader reader_check_unique = await command_id.ExecuteReaderAsync();    //provjerava postoji li vec user s danim id

            if (reader_check_unique.HasRows)
            {
                return false;
            }

            reader_check_unique.Close();

            command.Parameters.Add("@User_id", SqlDbType.Int).Value = _user.User_id;
            command.Parameters.Add("@First_name", SqlDbType.VarChar).Value = _user.First_name;
            command.Parameters.Add("@Last_name", SqlDbType.VarChar).Value = _user.Last_name;
            command.Parameters.Add("@Email", SqlDbType.VarChar).Value = _user.Email;
            command.Parameters.Add("@Country", SqlDbType.VarChar).Value = _user.Country;


            await command.ExecuteNonQueryAsync();
            connection.Close();
            return true;

        }


        public async Task<bool> UpdateUserAsync(int User_id, UserModel user)
        {
            UserModel _user = new UserModel();
            _user.User_id = User_id;
            _user.First_name = user.First_name;
            _user.Last_name = user.Last_name;
            _user.Email = user.Email;
            _user.Country = user.Country;

            SqlCommand command_id = new SqlCommand("SELECT * FROM User_ WHERE User_id = @User_id ", connection);
            command_id.Parameters.Add("User_id", SqlDbType.Int).Value = User_id;


            await connection.OpenAsync();


            SqlDataReader reader = await command_id.ExecuteReaderAsync();

            if (!reader.HasRows)
            {
                return false;
            }

            reader.Close();

            SqlCommand command = new SqlCommand("UPDATE User_ SET First_name = @First_name, Last_name = @Last_name, " +
                                                 "Email = @Email, Country = @Country WHERE User_id = @User_id ;", connection);


            command.Parameters.Add("@User_id", SqlDbType.Int).Value = User_id;
            command.Parameters.Add("@First_name", SqlDbType.VarChar).Value = _user.First_name;
            command.Parameters.Add("@Last_name", SqlDbType.VarChar).Value = _user.Last_name;
            command.Parameters.Add("@Email", SqlDbType.VarChar).Value = _user.Email;
            command.Parameters.Add("@Country", SqlDbType.VarChar).Value = _user.Country;

            await command.ExecuteNonQueryAsync();
            connection.Close();
            return true;
        }

        public async Task<bool> DeleteUserAsync(int User_id)
        {
            SqlCommand commandId = new SqlCommand("SELECT * FROM User_ WHERE User_id = @User_id; ", connection);
            SqlCommand command = new SqlCommand();
            await connection.OpenAsync();

            commandId.Parameters.Add("@User_id", SqlDbType.Int).Value = User_id;
            SqlDataReader reader = await commandId.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {

                    command = new SqlCommand("DELETE FROM User_ WHERE User_id = @User_id; ", connection);
                    command.Parameters.Add("@User_id", SqlDbType.Int).Value = User_id;
                }
            }

            else
            {
                return false;
            }
            reader.Close();
            await command.ExecuteNonQueryAsync();
            connection.Close();
            return true;
        }
    }
}
