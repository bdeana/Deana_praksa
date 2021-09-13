using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISDBModel.Common
{
    public interface IReviewModel
    {
        string Comment_ { get; set; }
        int Grade { get; set; }
        int User_fk { get; set; }
        int Song_fk { get; set; }

    }
}
