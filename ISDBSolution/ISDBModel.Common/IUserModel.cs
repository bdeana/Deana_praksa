using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISDBModel.Common
{
   public interface IUserModel
    {
        int User_id { get; set; }
        string First_name { get; set; }
        string Last_name { get; set; }
        string Email { get; set; }
        string Country { get; set; }
    }
}
