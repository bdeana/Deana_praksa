using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Model;
using WebApplication1.Repository;
using WebApplication1Service.Common;
using WebApplication1Repository.Common;

namespace WebApplication1.Service
{
    public class Class1 : IService
    {

        Class1R personR = new Class1R();
        public async Task<List<Person>> Get(Sorter sorter, Pager pager, PersonFilter personFilter)
        {
            return await Class1R.Get(sorter,pager,personFilter);
        }

        public async Task<Person> Get(string OIB)
        {
            return await Class1R.Get(OIB);
        }
        public async Task<bool> Post(string OIB, string mail)
        {
            return await Class1R.Post(OIB, mail);
        }
        public async Task<bool> Put(string OIB, string firstName, string lastName, string mail)
        {
            return await Class1R.Put(OIB, firstName, lastName, mail);
        }

        public async Task<bool> Delete(string OIB)
        {
            return await Class1R.Delete(OIB);
        }
    }
}
