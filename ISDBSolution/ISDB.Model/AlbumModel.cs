using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISDBModel.Common;

namespace ISDB.Model
{
    public class Album : IAlbum
    {

        public int Album_id { get; set; }
        public string Album_name { get; set; }
        public int Release_year { get; set; }

    }



}
