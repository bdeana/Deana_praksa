using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using ISDB.Model;
using ISDBRepository.Common;
using ISDB.Common;
using System.Data;

namespace ISDB.Repository
{
    public class AwardRepository : IAwardRepository
    {
        SqlConnection connection = new SqlConnection ("Data Source=E\\SQLEXPRESS;Initial Catalog=ISDB;Integrated Security=True");

        public async Task<List<Award>> GetAwardsAsync(Sorter sorter = null, Pager pager = null, AwardFilter awardFilter = null)
        {
            Sorter Sorting = new Sorter();
            string sort = Sorting.SortBy(sorter.Order_by, sorter.Sort_Order);
            Pager Paging = new Pager();
            string page = Paging.Page(pager.Page_number, pager.Page_size);
            AwardFilter Filter = new AwardFilter();
            string filter = Filter.FilterLike(awardFilter.Filter);

            SqlCommand command = new SqlCommand
                ("SELECT * FROM Award " + filter + sort + page + " ;", connection);
            await connection.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            List<Award> list_of_awards = new List<Award>();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    Award award = new Award();
                    award.Award_id = reader.GetInt32(0);
                    award.Award_name = reader.GetString(1);
                    award.Reciving_year = reader.GetInt32(2);
                    award.Song_fk = reader.GetInt32(3);
                    award.Album_fk = reader.GetInt32(4);


                    list_of_awards.Add(award);
                }
                reader.Close();
                connection.Close();
                return list_of_awards;
            }
            else
            {
                reader.Close();
                connection.Close();
                return null;
            }
        }
        public async Task<Award> GetAwardByIdAsync(int award_id)
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand
                ("SELECT * FROM Award WHERE Award_id = @award_id;", connection);

            SqlParameter id_parameter = new SqlParameter("@award_id", System.Data.SqlDbType.Int);

            command.Parameters.Add(id_parameter).Value = award_id;

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                Award award = new Award();
                while (await reader.ReadAsync())
                {
                    award.Award_id = award_id;
                    award.Award_name = reader.GetString(1);
                    award.Reciving_year = reader.GetInt32(2);
                    award.Album_fk = reader.GetInt32(3);
                    award.Song_fk = reader.GetInt32(4);


                }
                connection.Close();
                return award;
            }
            else
            {
                connection.Close();
                return null;
            }
        }

        public async Task<bool> CreateAwardAsync(Award award)
        {
            Award _award = new Award();
            _award.Award_id = award.Award_id;
            _award.Award_name = award.Award_name;
            _award.Reciving_year = award.Reciving_year;
            _award.Album_fk = award.Album_fk;
            _award.Song_fk = award.Song_fk;
            

            SqlCommand command = new SqlCommand("INSERT INTO Award (Award_id, Award_name , Reciving_year , Album_fk, Song_fk )" +
                                                "VALUES (@Award_id, @Award_name , @Reciving_year , @Album_fk, @Song_fk)", connection);

            SqlCommand command_id = new SqlCommand("SELECT * FROM Award WHERE Award_id =@Award_id ", connection);
            command_id.Parameters.Add("@Award_id", SqlDbType.Int).Value = _award.Award_id;
            await connection.OpenAsync();

            SqlDataReader reader = await command_id.ExecuteReaderAsync();     

            if (reader.HasRows)
            {
                return false;
            }

            reader.Close();

            command.Parameters.Add("@Award_id", SqlDbType.Int).Value = _award.Award_id;
            command.Parameters.Add("@Award_name", SqlDbType.VarChar).Value = _award.Award_name;
            command.Parameters.Add("@Reciving_year", SqlDbType.Int).Value = _award.Reciving_year;
            command.Parameters.Add("@Album_fk", SqlDbType.Int).Value = _award.Album_fk;
            command.Parameters.Add("@Song_fk", SqlDbType.Int).Value = _award.Song_fk;


            await command.ExecuteNonQueryAsync();
            connection.Close();

            return true;

        }


        public async Task<bool> UpdateAwardAsync(int award_id, Award award)
        {
            Award _award = new Award();
            _award.Award_id = award_id;
            _award.Award_name = award.Award_name;
            _award.Reciving_year = award.Reciving_year;
            _award.Album_fk = award.Album_fk;
            _award.Song_fk = award.Song_fk;

            SqlCommand command_id = new SqlCommand("SELECT * FROM Award WHERE Award_id =@Award_id ", connection);
            command_id.Parameters.Add("@Award_id", SqlDbType.Int).Value = award_id;
            await connection.OpenAsync();

            SqlDataReader reader = await command_id.ExecuteReaderAsync();

            if (!reader.HasRows)
            {
                return false;
            }

            reader.Close();

            SqlCommand command = new SqlCommand("UPDATE Award SET Award_name = @Award_name, Reciving_year = @Reciving_year, " +
                                                 "Album_fk= @Album_fk, Song_fk= @Song_fk WHERE Award_id= @Award_id", connection);

            command.Parameters.Add("Award_name", SqlDbType.VarChar).Value = _award.Award_name;
            command.Parameters.Add("Reciving_year", SqlDbType.Int).Value = _award.Reciving_year;
            command.Parameters.Add("Album_fk", SqlDbType.Int).Value = _award.Album_fk;
            command.Parameters.Add("Song_fk", SqlDbType.Int).Value = _award.Song_fk;
            command.Parameters.Add("Award_id", SqlDbType.Int).Value = award_id;

            await command.ExecuteNonQueryAsync();
            connection.Close();
            return true;
        }

        public async Task<bool> DeleteAwardAsync(int award_id)
        {
            SqlCommand commandId = new SqlCommand("SELECT * FROM Award WHERE Award_id=@award_id; ", connection);
            SqlCommand command = new SqlCommand();
            await connection.OpenAsync();


            commandId.Parameters.Add("@award_id", SqlDbType.Int).Value = award_id;


            

            SqlDataReader reader = await commandId.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {


                    command = new SqlCommand("DELETE FROM Award WHERE Award_id=@award_id; ", connection);
                    command.Parameters.Add("@award_id", SqlDbType.Int).Value = award_id;
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
