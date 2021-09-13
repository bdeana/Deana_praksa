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
    public class ReviewRepository : IReviewRepository


    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-BB9OT7S\\SQLEXPRESS;Initial Catalog=ISDB;Integrated Security=True");


        public async Task<List<ReviewModel>> GetReviewAsync(Sorter sorter = null, Pager pager = null, ReviewFilter reviewFilter = null)
        {


            Sorter Sorting = new Sorter();
            string sort = Sorting.SortBy(sorter.Order_by, sorter.Sort_Order);

            Pager Paging = new Pager();
            string page = Paging.Page(pager.Page_number, pager.Page_size);

            ReviewFilter Filter = new ReviewFilter();
            string filter = Filter.FilterLike(reviewFilter.Filter);


            SqlCommand command = new SqlCommand
                ("SELECT * FROM Review " + filter + sort + page + " ;", connection);
            await connection.OpenAsync();


            SqlDataReader reader = await command.ExecuteReaderAsync();
            List<ReviewModel> list_of_reviews = new List<ReviewModel>();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    ReviewModel review = new ReviewModel();
                    review.Comment_ = reader.GetString(0);
                    review.Grade = reader.GetInt32(1);
                    review.User_fk = reader.GetInt32(2);
                    review.Song_fk = reader.GetInt32(3);

                    list_of_reviews.Add(review);
                }
                reader.Close();
                connection.Close();
                return list_of_reviews;
            }
            else
            {
                reader.Close();
                connection.Close();
                return null;
            }
        }


        public async Task<ReviewModel> GetReviewByIdAsync(int User_fk, int Song_fk)
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand
                ("SELECT * FROM Review WHERE User_fk = @User_fk AND Song_fk = @Song_fk;", connection);

            command.Parameters.Add("@User_fk", SqlDbType.Int).Value = User_fk;
            command.Parameters.Add("@Song_fk", SqlDbType.Int).Value = Song_fk;

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                ReviewModel review = new ReviewModel();
                while (await reader.ReadAsync())
                {

                    review.Comment_ = reader.GetString(0);
                    review.Grade = reader.GetInt32(1);
                    review.User_fk = reader.GetInt32(2);
                    review.Song_fk = reader.GetInt32(3);

                }
                connection.Close();
                return review; ;
            }
            else
            {
                connection.Close();
                return null;
            }
        }

        public async Task<bool> CreateReviewAsync(ReviewModel review)
        {

            ReviewModel _review = new ReviewModel();
            _review.Comment_ = review.Comment_;
            _review.Grade = review.Grade;
            _review.User_fk = review.User_fk;
            _review.Song_fk = review.Song_fk;


            SqlCommand command = new SqlCommand("INSERT INTO Review (Comment_, Grade , User_fk , Song_fk)" +
                                                "VALUES (@Comment_, @Grade, @User_fk, @Song_fk) ; ", connection);

            SqlCommand command_id = new SqlCommand("SELECT * FROM Review WHERE User_fk = @User_fk AND Song_fk = @Song_fk ; ", connection);
            command_id.Parameters.Add("User_fk", SqlDbType.Int).Value = _review.User_fk;
            command_id.Parameters.Add("Song_fk", SqlDbType.Int).Value = _review.Song_fk;

            SqlCommand command_song = new SqlCommand("SELECT * FROM Song WHERE Song_id = @Song_id ; ", connection);
            command_song.Parameters.Add("Song_id", SqlDbType.Int).Value = _review.Song_fk;

            SqlCommand command_user = new SqlCommand("SELECT * FROM User_ WHERE User_id = @User_id ; ", connection);
            command_user.Parameters.Add("User_id", SqlDbType.Int).Value = _review.User_fk;

            await connection.OpenAsync();

            SqlDataReader reader_check_unique = await command_id.ExecuteReaderAsync();    //provjerava postoji li vec review 

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

            SqlDataReader reader_check_user = await command_user.ExecuteReaderAsync();    //provjerava postoji li user

            if (!reader_check_user.HasRows)
            {
                return false;
            }

            reader_check_song.Close();

            command.Parameters.Add("@Comment_", SqlDbType.VarChar).Value = _review.Comment_;
            command.Parameters.Add("@Grade", SqlDbType.Int).Value = _review.Grade;
            command.Parameters.Add("@User_fk", SqlDbType.Int).Value = _review.User_fk;
            command.Parameters.Add("@Song_fk", SqlDbType.Int).Value = _review.Song_fk;

            await command.ExecuteNonQueryAsync();
            connection.Close();
            return true;

        }


        public async Task<bool> UpdateReviewAsync(int User_fk, int Song_fk, ReviewModel review)
        {
            ReviewModel _review = new ReviewModel();
            _review.Comment_ = review.Comment_;
            _review.Grade = review.Grade;
            _review.User_fk = User_fk;
            _review.Song_fk = Song_fk;

            SqlCommand command_id = new SqlCommand("SELECT * FROM Review WHERE User_fk = @User_fk AND Song_fk = @Song_fk ;", connection);
            command_id.Parameters.Add("User_fk", SqlDbType.Int).Value = User_fk;
            command_id.Parameters.Add("Song_fk", SqlDbType.Int).Value = Song_fk;


            await connection.OpenAsync();


            SqlDataReader reader = await command_id.ExecuteReaderAsync();

            if (!reader.HasRows)
            {
                return false;
            }

            reader.Close();

            SqlCommand command = new SqlCommand("UPDATE Review SET Comment_ = @Comment_, Grade = @Grade " +
                                                 "WHERE User_fk = @User_fk AND Song_fk = @Song_fk ;", connection);


            command.Parameters.Add("@User_fk", SqlDbType.Int).Value = User_fk;
            command.Parameters.Add("@Song_fk", SqlDbType.Int).Value = Song_fk;
            command.Parameters.Add("@Comment_", SqlDbType.VarChar).Value = _review.Comment_;
            command.Parameters.Add("@Grade", SqlDbType.Int).Value = _review.Grade;

            await command.ExecuteNonQueryAsync();
            connection.Close();
            return true;
        }

        public async Task<bool> DeleteReviewAsync(int User_fk, int Song_fk)
        {
            SqlCommand commandId = new SqlCommand("SELECT * FROM Review WHERE User_fk = @User_fk AND Song_fk = @Song_fk ; ", connection);
            SqlCommand command = new SqlCommand();
            await connection.OpenAsync();

            commandId.Parameters.Add("@User_fk", SqlDbType.Int).Value = User_fk;
            commandId.Parameters.Add("@Song_fk", SqlDbType.Int).Value = Song_fk;
            SqlDataReader reader = await commandId.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {

                    command = new SqlCommand("DELETE FROM Review WHERE User_fk = @User_fk AND Song_fk = @Song_fk ; ", connection);
                    command.Parameters.Add("@User_fk", SqlDbType.Int).Value = User_fk;
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
