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
    public class ManagerRepository : IManagerRepository
    {

        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-BB9OT7S\\SQLEXPRESS;Initial Catalog=ISDB;Integrated Security=True");


        public async Task<List<ManagerModel>> GetManagerAsync(Sorter sorter = null, Pager pager = null, ManagerFilter managerFilter = null)
        {


            Sorter Sorting = new Sorter();
            string sort = Sorting.SortBy(sorter.Order_by, sorter.Sort_Order);

            Pager Paging = new Pager();
            string page = Paging.Page(pager.Page_number, pager.Page_size);

            ManagerFilter Filter = new ManagerFilter();
            string filter = Filter.FilterLike(managerFilter.Filter);


            SqlCommand command = new SqlCommand
                ("SELECT * FROM Manager " + filter + sort + page + " ;", connection);
            await connection.OpenAsync();


            SqlDataReader reader = await command.ExecuteReaderAsync();
            List<ManagerModel> list_of_managers = new List<ManagerModel>();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    ManagerModel manager = new ManagerModel();
                    manager.Manager_id = reader.GetInt32(0);
                    manager.First_name = reader.GetString(1);
                    manager.Last_name = reader.GetString(2);
                    manager.email = reader.GetString(3);
                    manager.Company = reader.GetString(4);


                    list_of_managers.Add(manager);
                }
                reader.Close();
                connection.Close();
                return list_of_managers;
            }
            else
            {
                reader.Close();
                connection.Close();
                return null;
            }
        }


        public async Task<ManagerModel> GetManagerByIdAsync(int Manager_id)
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand
                ("SELECT * FROM Manager WHERE Manager_id = @Manager_id;", connection);

            SqlParameter id_parameter = new SqlParameter("@Manager_id", System.Data.SqlDbType.Int);

            command.Parameters.Add(id_parameter).Value = Manager_id;

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                ManagerModel manager = new ManagerModel();
                while (await reader.ReadAsync())
                {

                    manager.Manager_id = Manager_id;
                    manager.First_name = reader.GetString(1);
                    manager.Last_name = reader.GetString(2);
                    manager.email = reader.GetString(3);
                    manager.Company = reader.GetString(4);


                }
                connection.Close();
                return manager;
            }
            else
            {
                connection.Close();
                return null;
            }
        }

        public async Task<bool> CreateManagerAsync(ManagerModel manager)
        {

            ManagerModel _manager = new ManagerModel();
            _manager.Manager_id = manager.Manager_id;
            _manager.First_name = manager.First_name;
            _manager.Last_name = manager.Last_name;
            _manager.email = manager.email;
            _manager.Company = manager.Company;

            SqlCommand command = new SqlCommand("INSERT INTO Manager (Manager_id, First_name , Last_name , email, Company" +
                                                "VALUES (@Manager_id, @First_name, @Last_name, @email, @Company", connection);

            SqlCommand command_id = new SqlCommand("SELECT * FROM Manager WHERE Manager_id = @Manager_id ", connection);
            command_id.Parameters.Add("Manager_id", SqlDbType.Int).Value = _manager.Manager_id;

            await connection.OpenAsync();

            SqlDataReader reader_check_unique = await command_id.ExecuteReaderAsync();    //provjerava postoji li vec manager s danim id

            if (reader_check_unique.HasRows)
            {
                return false;
            }

            reader_check_unique.Close();

            command.Parameters.Add("@Manager_id", SqlDbType.Int).Value = _manager.Manager_id;
            command.Parameters.Add("@Manager_name", SqlDbType.VarChar).Value = _manager.First_name;
            command.Parameters.Add("@Release_year", SqlDbType.Int).Value = _manager.Last_name;
            command.Parameters.Add("@Language", SqlDbType.VarChar).Value = _manager.email;
            command.Parameters.Add("@Studio_location", SqlDbType.VarChar).Value = _manager.Company;

            await command.ExecuteNonQueryAsync();
            connection.Close();
            return true;

        }


        public async Task<bool> UpdateManagerAsync(int Manager_id, ManagerModel manager)
        {
            ManagerModel _manager = new ManagerModel();
            _manager.Manager_id = Manager_id;
            _manager.First_name = manager.First_name;
            _manager.Last_name = manager.Last_name;
            _manager.email = manager.email;
            _manager.Company = manager.Company;

            SqlCommand command_id = new SqlCommand("SELECT * FROM Manager WHERE Manager_id =@Manager_id ", connection);
            command_id.Parameters.Add("Manager_id", SqlDbType.Int).Value = Manager_id;

            
            await connection.OpenAsync();

            SqlDataReader reader_check_unique = await command_id.ExecuteReaderAsync();    //provjerava postoji li manager s danim id

            if (!reader_check_unique.HasRows)
            {
                return false;
            }

            reader_check_unique.Close();

            

            SqlCommand command = new SqlCommand("UPDATE Manager SET First_name = @First_name, Last_name = @Last_name, " +
                                                 "email = @email, Company = @Company" +
                                                 " WHERE Manager_id=@Manager_id", connection);


            command.Parameters.Add("@Manager_id", SqlDbType.Int).Value = Manager_id;
            command.Parameters.Add("@First_name", SqlDbType.VarChar).Value = _manager.First_name;
            command.Parameters.Add("@Last_name", SqlDbType.Int).Value = _manager.Last_name;
            command.Parameters.Add("@email", SqlDbType.VarChar).Value = _manager.email;
            command.Parameters.Add("@Company", SqlDbType.VarChar).Value = _manager.Company;

            await command.ExecuteNonQueryAsync();
            connection.Close();
            return true;
        }

        public async Task<bool> DeleteManagerAsync(int Manager_id)
        {
            SqlCommand commandId = new SqlCommand("SELECT * FROM Manager WHERE Manager_id=@Manager_id; ", connection);
            SqlCommand command = new SqlCommand();
            await connection.OpenAsync();

            commandId.Parameters.Add("@Manager_id", SqlDbType.Int).Value = Manager_id;
            SqlDataReader reader = await commandId.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {

                    command = new SqlCommand("DELETE FROM Manager WHERE Manager_id=@Manager_id; ", connection);
                    command.Parameters.Add("@Manager_id", SqlDbType.Int).Value = Manager_id;
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
