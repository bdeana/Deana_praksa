using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    public class Person
    {
        public Person () { }
        public Person(string OIB, string firstName, string lastName, string mail)
        {
            _oib = OIB;
            _firstName = firstName;
            _lastName = lastName;
            _mail = mail;
        }

        public string _oib { get; set; }
        public string _firstName { get; set; }
        public string _lastName { get; set; }
        public string _mail { get; set; }

    }
}
