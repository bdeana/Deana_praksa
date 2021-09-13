using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISDBModel.Common;

namespace ISDB.Model
{
    public class SongModel : ISongModel
    {
        public SongModel() { }

        public SongModel(int id, string name, int year, string language, string studio_location, int duration, int genre_fk, int album_fk, int production_company_fk)
        {
            Song_id = id;
            Song_name = name;
            Release_year = year;
            Language = language;
            Studio_location = studio_location;
            Duration = duration;
            Genre_fk = genre_fk;
            Album_fk = album_fk;
            Production_Company_fk = production_company_fk;
        }

        public int Song_id { get; set; }
        public string Song_name { get; set; }
        public int Release_year { get; set; }
        public string Language { get; set; }
        public string Studio_location { get; set; }
        public int Duration { get; set; }
        public int Genre_fk { get; set; }
        public int Album_fk { get; set; }
        public int Production_Company_fk { get; set; }
    }
}
