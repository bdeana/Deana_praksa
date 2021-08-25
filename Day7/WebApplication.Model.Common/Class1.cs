using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Model.Common
{
    public interface IModelCommon
    {
        string _oib { get; set; }
        string _firstName { get; set; }
        string _lastName { get; set; }
        string _mail { get; set; }
    }
}
