using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISDB.Common
{
    public class Song_ArtistFilter : ISong_ArtistFilter
    {
        public string Filter { get; set; }

        public string FilterLike(string Filter)
        {
            if (Filter != "")
            {
                return String.Format(" WHERE Song_fk LIKE '%{0}%' ", Filter);
            }
            return "";
        }

    }
}
