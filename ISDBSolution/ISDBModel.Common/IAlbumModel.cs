using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISDBModel.Common
{
    public interface IAlbum
    {
        int Album_id { get; set; }
        string Album_name { get; set; }
        int Release_year { get; set; }
        
    }
}
