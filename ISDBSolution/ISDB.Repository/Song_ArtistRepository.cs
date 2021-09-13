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
    public class Song_ArtistRepository : ISong_ArtistRepository
    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-BB9OT7S\\SQLEXPRESS;Initial Catalog=ISDB;Integrated Security=True");


        public async Task<List<Song_ArtistModel>> GetSong_ArtistAsync(Sorter sorter = null, Pager pager = null, Song_ArtistFilter song_artistFilter = null)
        {


            Sorter Sorting = new Sorter();
            string sort = Sorting.SortBy(sorter.Order_by, sorter.Sort_Order);

            Pager Paging = new Pager();
            string page = Paging.Page(pager.Page_number, pager.Page_size);

            Song_ArtistFilter Filter = new Song_ArtistFilter();
            string filter = Filter.FilterLike(song_artistFilter.Filter);


            SqlCommand command = new SqlCommand
                ("SELECT * FROM Song_Artist " + filter + sort + page + " ;", connection);
            await connection.OpenAsync();


            SqlDataReader reader = await command.ExecuteReaderAsync();
            List<Song_ArtistModel> list_of_song_artists = new List<Song_ArtistModel>();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    Song_ArtistModel song_artist = new Song_ArtistModel();
                    song_artist.Song_fk = reader.GetInt32(0);
                    song_artist.Artist_fk = reader.GetInt32(1);

                    list_of_song_artists.Add(song_artist);
                }
                reader.Close();
                connection.Close();
                return list_of_song_artists;
            }
            else
            {
                reader.Close();
                connection.Close();
                return null;
            }
        }


        public async Task<Song_ArtistModel> GetSong_ArtistByIdAsync(int Song_fk, int Artist_fk)
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand
                ("SELECT * FROM Song_Artist WHERE Song_fk = @Song_fk AND Artist_fk = @Artist_fk;", connection);

            command.Parameters.Add("@Artist_fk", SqlDbType.Int).Value = Artist_fk;
            command.Parameters.Add("@Song_fk", SqlDbType.Int).Value = Song_fk;

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                Song_ArtistModel song_artist = new Song_ArtistModel();
                while (await reader.ReadAsync())
                {

                    song_artist.Song_fk = reader.GetInt32(0);
                    song_artist.Artist_fk = reader.GetInt32(1);

                }
                connection.Close();
                return song_artist; ;
            }
            else
            {
                connection.Close();
                return null;
            }
        }

        public async Task<bool> CreateSong_ArtistAsync(Song_ArtistModel song_artist)
        {

            Song_ArtistModel _song_artist = new Song_ArtistModel();
            _song_artist.Song_fk = song_artist.Song_fk;
            _song_artist.Artist_fk = song_artist.Artist_fk;


            SqlCommand command = new SqlCommand("INSERT INTO Song_Artist (Song_fk, Artist_fk)" +
                                                "VALUES (@Song_fk, @Artist_fk) ; ", connection);

            SqlCommand command_id = new SqlCommand("SELECT * FROM Song_Artist WHERE Song_fk = @Song_fk AND Artist_fk = @Artist_fk ; ", connection);
            command_id.Parameters.Add("Song_fk", SqlDbType.Int).Value = _song_artist.Song_fk;
            command_id.Parameters.Add("Artist_fk", SqlDbType.Int).Value = _song_artist.Artist_fk;

            SqlCommand command_song = new SqlCommand("SELECT * FROM Song WHERE Song_id = @Song_id ; ", connection);
            command_song.Parameters.Add("Song_id", SqlDbType.Int).Value = _song_artist.Song_fk;

            SqlCommand command_artist = new SqlCommand("SELECT * FROM User_ WHERE User_id = @Artist_id ; ", connection);
            command_artist.Parameters.Add("Artist_id", SqlDbType.Int).Value = _song_artist.Artist_fk;

            await connection.OpenAsync();

            SqlDataReader reader_check_unique = await command_id.ExecuteReaderAsync();    //provjerava postoji li vec song_artist 

            if (reader_check_unique.HasRows)
            {
                return false;
            }

            reader_check_unique.Close();

            SqlDataReader reader_check_song = await command_song.ExecuteReaderAsync();    //provjerava postoji li pjesma

            if (!reader_check_song.HasRows)
            {
                return false;
            }

            reader_check_song.Close();

            SqlDataReader reader_check_user = await command_artist.ExecuteReaderAsync();    //provjerava postoji li user

            if (!reader_check_user.HasRows)
            {
                return false;
            }

            reader_check_song.Close();

            command.Parameters.Add("@Song_fk", SqlDbType.Int).Value = _song_artist.Song_fk;
            command.Parameters.Add("@Artist_fk", SqlDbType.Int).Value = _song_artist.Artist_fk;

            await command.ExecuteNonQueryAsync();
            connection.Close();
            return true;

        }


        public async Task<bool> UpdateSong_ArtistAsync(int Song_fk, int Artist_fk, Song_ArtistModel song_artist)
        {
            Song_ArtistModel _song_artist = new Song_ArtistModel();
            _song_artist.Song_fk = Song_fk;
            _song_artist.Artist_fk = Artist_fk;

            SqlCommand command_id = new SqlCommand("SELECT * FROM Song_Artist WHERE Song_fk = @Song_fk AND Artist_fk = @Artist_fk ;", connection);
            command_id.Parameters.Add("Song_fk", SqlDbType.Int).Value = Song_fk;
            command_id.Parameters.Add("Artist_fk", SqlDbType.Int).Value = Artist_fk;


            await connection.OpenAsync();


            SqlDataReader reader = await command_id.ExecuteReaderAsync();

            if (!reader.HasRows)
            {
                return false;
            }

            reader.Close();

            SqlCommand command = new SqlCommand("UPDATE Song_Artist SET Song_kf = @Song_fk" +
                                                 "WHERE Aritst_fk = @Artist_fk;", connection);


            command.Parameters.Add("@Song_fk", SqlDbType.Int).Value = Song_fk;
            command.Parameters.Add("@Artist_fk", SqlDbType.Int).Value = Artist_fk;

            await command.ExecuteNonQueryAsync();
            connection.Close();
            return true;
        }

        public async Task<bool> DeleteSong_ArtistAsync(int Song_fk, int Artist_fk)
        {
            SqlCommand commandId = new SqlCommand("SELECT * FROM Song_Artist WHERE Artist_fk = @Artist_fk AND Song_fk = @Song_fk ; ", connection);
            SqlCommand command = new SqlCommand();
            await connection.OpenAsync();

            commandId.Parameters.Add("@Artist_fk", SqlDbType.Int).Value = Artist_fk;
            commandId.Parameters.Add("@Song_fk", SqlDbType.Int).Value = Song_fk;
            SqlDataReader reader = await commandId.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {

                    command = new SqlCommand("DELETE FROM Song_Artist WHERE Artist_fk = @Artist_fk AND Song_fk = @Song_fk ; ", connection);
                    command.Parameters.Add("@Artist_fk", SqlDbType.Int).Value = Artist_fk;
                    command.Parameters.Add("@Song_fk", SqlDbType.Int).Value = Song_fk;
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
