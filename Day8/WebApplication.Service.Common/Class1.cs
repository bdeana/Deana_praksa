using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Model;

namespace WebApplication1Service.Common
{
    public class IService
    {
        Task<List<Person>> Get(Sorter sorter, Pager pager, PersonFilter personFilter);
        Task<Person> Get(string OIB);
        Task<bool> Post(string OIB, string mail);
        Task<bool> Put(string OIB, string firstName, string lastName, string mail);
        Task<bool> Delete(string OIB);

    }
}
