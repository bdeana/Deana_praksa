using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using ISDB.Model;
using ISDBRepository.Common;
using ISDB.Common;

namespace ISDB.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        
        SqlConnection connection = new SqlConnection("Data Source=E\\SQLEXPRESS;Initial Catalog=ISDB;Integrated Security=True");

        public async Task<List<Album>> GetAlbumsAsync(Sorter sorter = null, Pager pager = null, AlbumFilter albumFilter = null)
        {
            Sorter Sorting = new Sorter();
            string sort = Sorting.SortBy(sorter.Order_by, sorter.Sort_Order);
            Pager Paging = new Pager();
            string page = Paging.Page(pager.Page_number, pager.Page_size);
            AlbumFilter Filter = new AlbumFilter();
            string filter = Filter.FilterLike(albumFilter.Filter);

            SqlCommand command = new SqlCommand
                ("SELECT * FROM Album " + filter + sort + page + " ;", connection);
            await connection.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            List<Album> list_of_albums = new List<Album>();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    Album album = new Album();
                    album.Album_id = reader.GetInt32(0);
                    album.Album_name = reader.GetString(1);
                    album.Release_year = reader.GetInt32(2);


                    list_of_albums.Add(album);
                }
                reader.Close();
                connection.Close();
                return list_of_albums;
            }
            else
            {
                reader.Close();
                connection.Close();
                return null;
            }
        }

        public async Task<Album> GetAlbumByIdAsync(int album_id)
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand
                ("SELECT * FROM Album WHERE Album_id = @album_id;", connection);

            SqlParameter id_parameter = new SqlParameter("@album_id", System.Data.SqlDbType.Int);

            command.Parameters.Add(id_parameter).Value = album_id;

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                Album album = new Album();
                while (await reader.ReadAsync())
                {
                    album.Album_id = album_id;
                    album.Album_name = reader.GetString(1);
                    album.Release_year = reader.GetInt32(2);

                }
                connection.Close();
                return album;
            }
            else
            {
                connection.Close();
                return null;
            }
        }

        public async Task<bool> CreateAlbumAsync(Album album)
        {
            Album _album = new Album();
            _album.Album_id = album.Album_id;
            _album.Album_name = album.Album_name;
            _album.Release_year = album.Release_year;

            SqlCommand command = new SqlCommand("INSERT INTO Album (Album_id, Album_name , Release_year )" +
                                                "VALUES (@Album_id, @Album_name, @Release_year)", connection);

            SqlCommand command_id = new SqlCommand("SELECT * FROM Album WHERE Album_id =@Album_id ", connection);
            command_id.Parameters.Add("@Album_id", SqlDbType.Int).Value = _album.Album_id;
            await connection.OpenAsync();

            SqlDataReader reader = await command_id.ExecuteReaderAsync();      

            if (reader.HasRows)
            {
                return false;
            }

            reader.Close();

            command.Parameters.Add("@Album_id", SqlDbType.Int).Value = _album.Album_id;
            command.Parameters.Add("@Album_name", SqlDbType.VarChar).Value = _album.Album_name;
            command.Parameters.Add("@Release_year", SqlDbType.Int).Value = _album.Release_year;

            await command.ExecuteNonQueryAsync();
            connection.Close();

            return true;

        }


        public async Task<bool> UpdateAlbumAsync(int album_id, Album album)
        {
            Album _album = new Album();
            _album.Album_id = album_id;
            _album.Album_name = album.Album_name;
            _album.Release_year = album.Release_year;

            SqlCommand command_id = new SqlCommand("SELECT * FROM Album WHERE Album_id =@Album_id ", connection);
            command_id.Parameters.Add("@Album_id", SqlDbType.Int).Value = album_id;
            await connection.OpenAsync();

            SqlDataReader reader = await command_id.ExecuteReaderAsync();

            if (!reader.HasRows)
            {
                return false;
            }

            reader.Close();

            SqlCommand command = new SqlCommand("UPDATE Album SET Album_name = @Album_name, Release_year = @Release_year, " +
                                                 " WHERE Album_id= @Album_id", connection);

            command.Parameters.Add("Album_name", SqlDbType.VarChar).Value = _album.Album_name;
            command.Parameters.Add("Release_year", SqlDbType.Int).Value = _album.Release_year;
            command.Parameters.Add("Album_id", SqlDbType.Int).Value = album_id;

            await command.ExecuteNonQueryAsync();
            connection.Close();
            return true;
        }

        public async Task<bool> DeleteAlbumAsync(int album_id)
        {
            SqlCommand commandId = new SqlCommand("SELECT * FROM Album WHERE Album_id=@album_id; ", connection);
            SqlCommand command_check = new SqlCommand("SELECT * FROM Award WHERE Album_id = @album_id; ", connection);
            SqlCommand command = new SqlCommand();
            await connection.OpenAsync();


            commandId.Parameters.Add("@album_id", SqlDbType.Int).Value = album_id;
            command_check.Parameters.Add("@album_id", SqlDbType.Int).Value = album_id;

            SqlDataReader reader_check = await command_check.ExecuteReaderAsync();

            if (reader_check.HasRows)
            {
                return false;
            }

            reader_check.Close();

            SqlDataReader reader = await commandId.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {


                    command = new SqlCommand("DELETE FROM Album WHERE Album_id=@album_id; ", connection);
                    command.Parameters.Add("@album_id", SqlDbType.Int).Value = album_id;
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
