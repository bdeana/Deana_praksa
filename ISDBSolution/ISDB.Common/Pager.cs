using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISDB.Common
{
    public class Pager : IPager
    {
        public int Page_number { get; set; }
        public int Page_size { get; set; }

        public string Page(int Page_number, int Page_size)
        {
            if (Page_number > 0 && Page_size > 0)
            {
                return String.Format(" OFFSET {0} ROWS FETCH NEXT {1} ROW ONLY ", Page_size * (Page_number - 1), Page_size);
            }
            return "";
        }
    }
}
