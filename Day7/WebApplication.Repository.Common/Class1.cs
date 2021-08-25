using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Repository.Common
{
    public interface IRepositoryCommon
    {
        Task<List<Person>> Get();
        Task<Person> Get(string OIB);
        Task<bool> Post(string OIB, string mail);
        Task<bool> Put(string OIB, string firstName, string lastName, string mail);
        Task<bool> Delete(string OIB);


    }
}
