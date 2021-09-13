using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISDBModel.Common;

namespace ISDB.Model
{
    public class Award : IAward
    {
        public Award() { }
        public Award(int award_id, string award_name, int reciving_year, int song_fk, int album_fk)
        {
            Award_id = award_id;
            Award_name = award_name;
            Reciving_year = reciving_year;
            Song_fk = song_fk;
            Album_fk = album_fk;


        }

        public int Award_id { get; set; }
        public string Award_name { get; set; }
        public int Reciving_year { get; set; }
        public int Song_fk { get; set; }
        public int Album_fk { get; set; }
    }
}
