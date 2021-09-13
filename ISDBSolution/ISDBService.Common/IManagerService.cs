using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISDB.Model;
using ISDB.Common;

namespace ISDBService.Common
{
    public interface IManagerService
    {
        Task<List<ManagerModel>> GetManagerAsync(Sorter sorter, Pager pager, ManagerFilter managerFilter);
        Task<ManagerModel> GetManagerByIdAsync(int id);
        Task<bool> CreateManagerAsync(ManagerModel manager);
        Task<bool> UpdateManagerAsync(int id, ManagerModel manager);
        Task<bool> DeleteManagerAsync(int id);
    }
}
