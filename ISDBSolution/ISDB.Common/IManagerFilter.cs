using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISDB.Common
{
    public interface IManagerFilter
    {
        string Filter { get; set; }

        string FilterLike(string Filter);
    }
}
