using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISDB.Common
{
    public interface IPager
    {
        int Page_number { get; set; }
        int Page_size { get; set; }

        string Page(int Page_number, int Page_size);
    }
}
