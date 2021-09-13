using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISDB.Common
{
    public interface ISorter
    {
        string Order_by { get; set; }
        string Sort_Order { get; set; }
        string SortBy(string Order_by, string Sort_Order);
    }
}
