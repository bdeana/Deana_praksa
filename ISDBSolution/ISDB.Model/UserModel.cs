using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISDBModel.Common;

namespace ISDB.Model
{
    public class UserModel : IUserModel
    {

        public UserModel() { }

        public UserModel(int id, string name, string last_name, string email, string country)
        {
            User_id = id;
            First_name = name;
            Last_name = last_name;
            Email = email;
            Country = country;
        }

        public int User_id { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
    }
}
