using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISDBModel.Common
{
    public interface ISongModel
    {
        int Song_id { get; set; }
        string Song_name { get; set; }
        int Release_year { get; set; }
        string Language { get; set; }
        string Studio_location { get; set; }
        int Duration { get; set; }
        int Genre_fk { get; set; }
        int Album_fk { get; set; }
        int Production_Company_fk { get; set; }
    }
}
