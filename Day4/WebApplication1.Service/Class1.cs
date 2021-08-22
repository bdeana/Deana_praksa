using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Model;
using WebApplication1.Repository;

namespace WebApplication1.Service
{
    public class Class1
    {

        Class1R personR = new Class1R();
        public List<Person> Get()
        {
            return Class1R.Get();
        }

        public Person Get(string OIB)
        {
            return Class1R.Get(OIB);
        }
        public bool Post(string OIB, string mail)
        {
            return Class1R.Post(OIB, mail);
        }
        public bool Put(string OIB, string firstName, string lastName, string mail)
        {
            return Class1R.Put(OIB, firstName, lastName, mail);
        }

        public bool Delete(string OIB)
        {
            return Class1R.Delete(OIB);
        }
    }
}
