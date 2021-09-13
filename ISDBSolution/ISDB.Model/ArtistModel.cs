using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISDBModel.Common;
namespace ISDB.Model
{
    public class ArtistModel : IArtistModel
    {
        public ArtistModel() { }
        public ArtistModel(int id, string name, string country, string mail)
        {
            Artist_id = id;
            Artist_name = name;
            Country_of_origin = country;
            email = mail;
        }

        public int Artist_id { get; set; }
        public string Artist_name { get; set; }
        public string Country_of_origin { get; set; }
        public string email { get; set; }
    }
}
