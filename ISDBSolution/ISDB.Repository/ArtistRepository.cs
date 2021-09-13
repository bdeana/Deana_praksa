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
    public class ArtistRepository : IArtistRepository
    {

        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-BB9OT7S\\SQLEXPRESS;Initial Catalog=ISDB;Integrated Security=True");


        public async Task<List<ArtistModel>> GetArtistAsync(Sorter sorter = null, Pager pager = null, ArtistFilter artistFilter = null)
        {


            Sorter Sorting = new Sorter();
            string sort = Sorting.SortBy(sorter.Order_by, sorter.Sort_Order);

            Pager Paging = new Pager();
            string page = Paging.Page(pager.Page_number, pager.Page_size);

            ArtistFilter Filter = new ArtistFilter();
            string filter = Filter.FilterLike(artistFilter.Filter);


            SqlCommand command = new SqlCommand
                ("SELECT * FROM Artist " + filter + sort + page + " ;", connection);
            await connection.OpenAsync();


            SqlDataReader reader = await command.ExecuteReaderAsync();
            List<ArtistModel> list_of_artists = new List<ArtistModel>();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    ArtistModel artist = new ArtistModel();
                    artist.Artist_id = reader.GetInt32(0);
                    artist.Artist_name = reader.GetString(1);
                    artist.Country_of_origin = reader.GetString(2);
                    artist.email = reader.GetString(3);


                    list_of_artists.Add(artist);
                }
                reader.Close();
                connection.Close();
                return list_of_artists;
            }
            else
            {
                reader.Close();
                connection.Close();
                return null;
            }
        }


        public async Task<ArtistModel> GetArtistByIdAsync(int Artist_id)
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand
                ("SELECT * FROM Artist WHERE Artist_id = @Artist_id;", connection);

            SqlParameter id_parameter = new SqlParameter("@Artist_id", System.Data.SqlDbType.Int);

            command.Parameters.Add(id_parameter).Value = Artist_id;

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                ArtistModel artist = new ArtistModel();
                while (await reader.ReadAsync())
                {

                    artist.Artist_id = Artist_id;
                    artist.Artist_name = reader.GetString(1);
                    artist.Country_of_origin = reader.GetString(2);
                    artist.email = reader.GetString(3);


                }
                connection.Close();
                return artist;
            }
            else
            {
                connection.Close();
                return null;
            }
        }

        public async Task<bool> CreateArtistAsync(ArtistModel artist)
        {

            ArtistModel _artist = new ArtistModel();
            _artist.Artist_id = artist.Artist_id;
            _artist.Artist_name = artist.Artist_name;
            _artist.Country_of_origin = artist.Country_of_origin;
            _artist.email = artist.email;

            SqlCommand command = new SqlCommand("INSERT INTO Artist (Artist_id, Artist_name , Country_of_origin , email" +
                                                "VALUES (@Artist_id, @Artist_name, @Country_of_origin, @email", connection);

            SqlCommand command_id = new SqlCommand("SELECT * FROM Artist WHERE Artist_id = @Artist_id ", connection);
            command_id.Parameters.Add("Artist_id", SqlDbType.Int).Value = _artist.Artist_id;

            await connection.OpenAsync();

            SqlDataReader reader_check_unique = await command_id.ExecuteReaderAsync();    //provjerava postoji li vec artist s danim id

            if (reader_check_unique.HasRows)
            {
                return false;
            }

            reader_check_unique.Close();

            command.Parameters.Add("@Artist_id", SqlDbType.Int).Value = _artist.Artist_id;
            command.Parameters.Add("@Artist_name", SqlDbType.VarChar).Value = _artist.Artist_name;
            command.Parameters.Add("@Release_year", SqlDbType.Int).Value = _artist.Country_of_origin;
            command.Parameters.Add("@Language", SqlDbType.VarChar).Value = _artist.email;

            await command.ExecuteNonQueryAsync();
            connection.Close();
            return true;

        }


        public async Task<bool> UpdateArtistAsync(int Artist_id, ArtistModel artist)
        {
            ArtistModel _artist = new ArtistModel();
            _artist.Artist_id = Artist_id;
            _artist.Artist_name = artist.Artist_name;
            _artist.Country_of_origin = artist.Country_of_origin;
            _artist.email = artist.email;

            SqlCommand command_id = new SqlCommand("SELECT * FROM Artist WHERE Artist_id =@Artist_id ", connection);
            command_id.Parameters.Add("Artist_id", SqlDbType.Int).Value = Artist_id;


            await connection.OpenAsync();

            SqlDataReader reader_check_unique = await command_id.ExecuteReaderAsync();    //provjerava postoji li artist s danim id

            if (!reader_check_unique.HasRows)
            {
                return false;
            }

            reader_check_unique.Close();



            SqlCommand command = new SqlCommand("UPDATE Artist SET Artist_name = @Artist_name, Country_of_origin = @Country_of_origin, " +
                                                 "email = @email" +
                                                 " WHERE Artist_id=@Artist_id", connection);


            command.Parameters.Add("@Artist_id", SqlDbType.Int).Value = Artist_id;
            command.Parameters.Add("@Artist_name", SqlDbType.VarChar).Value = _artist.Artist_name;
            command.Parameters.Add("@Country_of_origin", SqlDbType.Int).Value = _artist.Country_of_origin;
            command.Parameters.Add("@email", SqlDbType.VarChar).Value = _artist.email;

            await command.ExecuteNonQueryAsync();
            connection.Close();
            return true;
        }

        public async Task<bool> DeleteArtistAsync(int Artist_id)
        {
            SqlCommand commandId = new SqlCommand("SELECT * FROM Artist WHERE Artist_id=@Artist_id; ", connection);
            SqlCommand command = new SqlCommand();
            await connection.OpenAsync();

            commandId.Parameters.Add("@Artist_id", SqlDbType.Int).Value = Artist_id;
            SqlDataReader reader = await commandId.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {

                    command = new SqlCommand("DELETE FROM Artist WHERE Artist_id=@Artist_id; ", connection);
                    command.Parameters.Add("@Artist_id", SqlDbType.Int).Value = Artist_id;
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
