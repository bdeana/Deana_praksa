using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISDB.Common
{
    public class Sorter : ISorter
    {
        public string Order_by { get; set; }
        public string Sort_Order { get; set; }

        public string SortBy(string Order_by, string Sort_Order)
        {
            if (Order_by != "" && Sort_Order != "")
            {
                return String.Format(" ORDER BY {0} {1} ", Order_by, Sort_Order);
            }
            return "";
        }
    }
}


