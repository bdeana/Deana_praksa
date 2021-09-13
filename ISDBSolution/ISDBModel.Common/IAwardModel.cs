using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISDBModel.Common
{
    public interface IAward
    {
        int Award_id { get; set; }
        string Award_name { get; set; }
        int Reciving_year { get; set; }
        int Song_fk { get; set; }
        int Album_fk { get; set; }
    }
}
