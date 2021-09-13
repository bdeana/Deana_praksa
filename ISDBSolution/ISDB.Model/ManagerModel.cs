using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISDBModel.Common;

namespace ISDB.Model
{
    public class ManagerModel : IManagerModel
    {
        public ManagerModel() { }

        public ManagerModel(int id, string first_name, string last_name, string _email, string company)
        {
            Manager_id = id;
            First_name = first_name;
            Last_name = last_name;
            email = _email;
            Company = company;
        }

        public int Manager_id { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string email { get; set; }
        public string Company { get; set; }
    }
}
