using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISDB.Common
{
    public interface ISong_ArtistFilter
    {
        string Filter { get; set; }

        string FilterLike(string Filter);
    }
}
