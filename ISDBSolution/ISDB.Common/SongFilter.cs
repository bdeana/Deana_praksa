using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISDB.Common
{
    public class SongFilter : ISongFilter
    {
        public string Filter { get; set; }

        public string FilterLike(string Filter)
        {
            if (Filter != "")
            {
                return String.Format(" WHERE Song_name LIKE '%{0}%' ", Filter);
            }
            return "";
        }
    }

}