using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISDBModel.Common;

namespace ISDB.Model
{
    public class ReviewModel: IReviewModel
    {

        public ReviewModel() { }

        public ReviewModel(string comment, int grade, int user_fk, int song_fk)
        {
            Comment_ = comment;
            Grade = grade;
            User_fk = user_fk;
            Song_fk = song_fk;
        }

        public string Comment_ { get; set; }
        public int Grade { get; set; }
        public int User_fk { get; set; }
        public int Song_fk { get; set; }
    }
}
