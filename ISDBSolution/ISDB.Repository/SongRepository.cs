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
    public class SongRepository : ISongRepository   //pjesma bi takoder trebala imati atribut ocjena sto bi bio prosjek svih ocjena za tu pjesmu
                                                    //mozda i ukupan broj glasova


    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-BB9OT7S\\SQLEXPRESS;Initial Catalog=ISDB;Integrated Security=True");


        public async Task<List<SongModel>> GetSongAsync(Sorter sorter = null, Pager pager = null, SongFilter songFilter = null)
        {

            
            Sorter Sorting = new Sorter();
            string sort = Sorting.SortBy(sorter.Order_by, sorter.Sort_Order);

            Pager Paging = new Pager();
            string page = Paging.Page(pager.Page_number, pager.Page_size);

            SongFilter Filter = new SongFilter();
            string filter = Filter.FilterLike(songFilter.Filter);

            
            SqlCommand command = new SqlCommand
                ("SELECT * FROM Song "  + filter + sort + page + " ;", connection);
            await connection.OpenAsync();


            SqlDataReader reader = await command.ExecuteReaderAsync();
            List<SongModel> list_of_songs = new List<SongModel>();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    SongModel song = new SongModel();
                    song.Song_id = reader.GetInt32(0);
                    song.Song_name = reader.GetString(1);
                    song.Release_year = reader.GetInt32(2);
                    song.Language = reader.GetString(3);
                    song.Studio_location = reader.GetString(4);
                    song.Duration = reader.GetInt32(5);
                    song.Genre_fk = reader.GetInt32(6);
                    song.Album_fk = reader.GetInt32(7);
                    song.Production_Company_fk = reader.GetInt32(8);


                    list_of_songs.Add(song);
                }
                reader.Close();
                connection.Close();
                return list_of_songs;
            }
            else
            {
                reader.Close();
                connection.Close();
                return null;
            }
        }


        public async Task<SongModel> GetSongByIdAsync(int Song_id)
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand
                ("SELECT * FROM Song WHERE Song_id = @Song_id;", connection);

            SqlParameter id_parameter = new SqlParameter("@Song_id", System.Data.SqlDbType.Int);

            command.Parameters.Add(id_parameter).Value = Song_id;

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                SongModel song = new SongModel();
                while (await reader.ReadAsync())
                {

                    song.Song_id = Song_id;
                    song.Song_name = reader.GetString(1);
                    song.Release_year = reader.GetInt32(2);
                    song.Language = reader.GetString(3);
                    song.Studio_location = reader.GetString(4);
                    song.Duration = reader.GetInt32(5);
                    song.Genre_fk = reader.GetInt32(6);
                    song.Album_fk = reader.GetInt32(7);
                    song.Production_Company_fk = reader.GetInt32(8);


                }
                connection.Close();
                return song;
            }
            else
            {
                connection.Close();
                return null;
            }
        }

        public async Task<bool> CreateSongAsync(SongModel song)
        {

            SongModel _song = new SongModel();
            _song.Song_id = song.Song_id;
            _song.Song_name = song.Song_name;
            _song.Release_year = song.Release_year;
            _song.Language = song.Language;
            _song.Studio_location = song.Studio_location;
            _song.Duration = song.Duration;
            _song.Genre_fk = song.Genre_fk;
            _song.Album_fk = song.Album_fk;
            _song.Production_Company_fk = song.Production_Company_fk;

            SqlCommand command = new SqlCommand("INSERT INTO Song (Song_id, Song_name , Release_year , Language, Studio_location, Duration," +
                                                                    "Genre_fk, Album_fk, Production_Company_fk )" +
                                                "VALUES (@Song_id, @Song_name, @Release_year, @Language, @Studio_location, @Duration" +
                                                                     "@Genre_fk, @Album_fk, @Production_Company_fk); ", connection);

            SqlCommand command_id = new SqlCommand("SELECT * FROM Song WHERE Song_id = @Song_id ", connection);
            command_id.Parameters.Add("Song_id", SqlDbType.Int).Value = _song.Song_id;


            SqlCommand command_check_genre = new SqlCommand("SELECT * FROM Song WHERE Genre_fk = @Genre_fk; ", connection);
            command_check_genre.Parameters.Add("Genre_fk", SqlDbType.Int).Value = _song.Genre_fk;

            SqlCommand command_check_album = new SqlCommand("SELECT * FROM Song WHERE Album_fk = @Album_fk; ", connection);
            command_check_album.Parameters.Add("Album_fk", SqlDbType.Int).Value = _song.Album_fk;

            SqlCommand command_check_production_company = new SqlCommand("SELECT * FROM Song WHERE " +
                                                                        "Production_Company_fk = @Production_Company_fk; ", connection);
            command_check_production_company.Parameters.Add("Album_fk", SqlDbType.Int).Value = _song.Production_Company_fk;

            await connection.OpenAsync();

            SqlDataReader reader_check_unique = await command_id.ExecuteReaderAsync();    //provjerava postoji li vec song s danim id

            if (reader_check_unique.HasRows)
            {
                return false;
            }

            reader_check_unique.Close();

            SqlDataReader reader_check_genre = await command_check_genre.ExecuteReaderAsync();    //provjerava je li zanr postojec, inace ju ne moze dodati

            if (!reader_check_genre.HasRows)
            {
                return false;
            }

            reader_check_genre.Close();

            SqlDataReader reader_check_album = await command_check_album.ExecuteReaderAsync();    //provjerava je li album postojec, inace ju ne moze dodati

            if (!reader_check_album.HasRows)
            {
                return false;
            }

            reader_check_album.Close();

            SqlDataReader reader_check_production_company = await command_check_production_company.ExecuteReaderAsync();    //provjerava je li p_company postojeca, inace ju ne moze dodati

            if (!reader_check_production_company.HasRows)
            {
                return false;
            }

            reader_check_production_company.Close();

            command.Parameters.Add("@Song_id", SqlDbType.Int).Value = _song.Song_id;
            command.Parameters.Add("@Song_name", SqlDbType.VarChar).Value = _song.Song_name;
            command.Parameters.Add("@Release_year", SqlDbType.Int).Value = _song.Release_year;
            command.Parameters.Add("@Language", SqlDbType.VarChar).Value = _song.Language;
            command.Parameters.Add("@Studio_location", SqlDbType.VarChar).Value = _song.Studio_location;
            command.Parameters.Add("@Duration", SqlDbType.Int).Value = _song.Duration;
            command.Parameters.Add("@Genre_fk", SqlDbType.Int).Value = _song.Genre_fk;
            command.Parameters.Add("@Album_fk", SqlDbType.Int).Value = _song.Album_fk;
            command.Parameters.Add("@Production_Company_fk", SqlDbType.Int).Value = _song.Production_Company_fk;


            await command.ExecuteNonQueryAsync();
            connection.Close();
            return true;

        }


        public async Task<bool> UpdateSongAsync(int Song_id, SongModel song)
        {
            SongModel _song = new SongModel();
            _song.Song_id = Song_id;
            _song.Song_name = song.Song_name;
            _song.Release_year = song.Release_year;
            _song.Language = song.Language;
            _song.Studio_location = song.Studio_location;
            _song.Duration = song.Duration;
            _song.Genre_fk = song.Genre_fk;
            _song.Album_fk = song.Album_fk;
            _song.Production_Company_fk = song.Production_Company_fk;

            SqlCommand command_id = new SqlCommand("SELECT * FROM Song WHERE Song_id =@Song_id ", connection);
            command_id.Parameters.Add("Song_id", SqlDbType.Int).Value = Song_id;

            SqlCommand command_check_genre = new SqlCommand("SELECT * FROM Song WHERE Genre_fk = @Genre_fk; ", connection);
            command_check_genre.Parameters.Add("Genre_fk", SqlDbType.Int).Value = _song.Genre_fk;

            SqlCommand command_check_album = new SqlCommand("SELECT * FROM Song WHERE Album_fk = @Album_fk; ", connection);
            command_check_album.Parameters.Add("Album_fk", SqlDbType.Int).Value = _song.Album_fk;

            SqlCommand command_check_production_company = new SqlCommand("SELECT * FROM Song WHERE " +
                                                                        "Production_Company_fk = @Production_Company_fk; ", connection);
            command_check_production_company.Parameters.Add("Album_fk", SqlDbType.Int).Value = _song.Production_Company_fk;

            await connection.OpenAsync();

            SqlDataReader reader_check_unique = await command_id.ExecuteReaderAsync();    //provjerava postoji li song s danim id

            if (!reader_check_unique.HasRows)
            {
                return false;
            }

            reader_check_unique.Close();

            SqlDataReader reader_check_genre = await command_check_genre.ExecuteReaderAsync();    //provjerava je li zanr postojec, inace ju ne moze dodati

            if (!reader_check_genre.HasRows)
            {
                return false;
            }

            reader_check_genre.Close();

            SqlDataReader reader_check_album = await command_check_album.ExecuteReaderAsync();    //provjerava je li album postojec, inace ju ne moze dodati

            if (!reader_check_album.HasRows)
            {
                return false;
            }

            reader_check_album.Close();

            SqlDataReader reader_check_production_company = await command_check_production_company.ExecuteReaderAsync();    //provjerava je li p_company postojeca, inace ju ne moze dodati

            if (!reader_check_production_company.HasRows)
            {
                return false;
            }

            reader_check_production_company.Close();

            SqlCommand command = new SqlCommand("UPDATE Song SET Song_name = @Song_name, Release_year = @Release_year, " +
                                                 "Language = @Language, Studio_location = @Studio_location, Duration = @Duration," +
                                                 "Genre_fk = @Genre_fk, Album_fk = @Album_fk, Production_Company_fk = @Production_Company_fk" +
                                                 " WHERE Song_id=@Song_id", connection);


            command.Parameters.Add("@Song_id", SqlDbType.Int).Value = Song_id;
            command.Parameters.Add("@Song_name", SqlDbType.VarChar).Value = _song.Song_name;
            command.Parameters.Add("@Release_year", SqlDbType.Int).Value = _song.Release_year;
            command.Parameters.Add("@Language", SqlDbType.VarChar).Value = _song.Language;
            command.Parameters.Add("@Studio_location", SqlDbType.VarChar).Value = _song.Studio_location;
            command.Parameters.Add("@Duration", SqlDbType.Int).Value = _song.Duration;
            command.Parameters.Add("@Genre_fk", SqlDbType.Int).Value = _song.Genre_fk;
            command.Parameters.Add("@Album_fk", SqlDbType.Int).Value = _song.Album_fk;
            command.Parameters.Add("@Production_Company_fk", SqlDbType.Int).Value = _song.Production_Company_fk;

            await command.ExecuteNonQueryAsync();
            connection.Close();
            return true;
        }

        public async Task<bool> DeleteSongAsync(int Song_id)
        {
            SqlCommand commandId = new SqlCommand("SELECT * FROM Song WHERE Song_id=@Song_id; ", connection);
            SqlCommand command = new SqlCommand();
            await connection.OpenAsync();

            commandId.Parameters.Add("@Song_id", SqlDbType.Int).Value = Song_id;
            SqlDataReader reader = await commandId.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {

                    command = new SqlCommand("DELETE FROM Song WHERE Song_id=@Song_id; ", connection);
                    command.Parameters.Add("@Song_id", SqlDbType.Int).Value = Song_id;
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
