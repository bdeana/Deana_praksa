using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISDBModel.Common;

namespace ISDB.Model
{
    public class Song_ArtistModel : ISong_ArtistModel
    {
        public Song_ArtistModel() { }

        public Song_ArtistModel(int song_fk, int artist_fk)
        {
            Song_fk = song_fk;
            Artist_fk = artist_fk;
        }

        public int Song_fk { get; set; }
        public int Artist_fk { get; set; }
    }
}
