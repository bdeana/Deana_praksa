using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISDB.Model;
using ISDB.Repository;
using ISDBService.Common;
using ISDBRepository.Common;
using ISDB.Common;

namespace ISDB.Service
{
    public class ManagerService : IManagerService
    {
        IManagerRepository _managerRepository;

        public ManagerService(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

        public async Task<List<ManagerModel>> GetManagerAsync(Sorter sorter, Pager pager, ManagerFilter managerFilter)
        {
            return await _managerRepository.GetManagerAsync(sorter, pager, managerFilter);

        }

        public async Task<ManagerModel> GetManagerByIdAsync(int id)
        {
            return await _managerRepository.GetManagerByIdAsync(id);
        }

        public async Task<bool> CreateManagerAsync(ManagerModel manager)
        {
            return await _managerRepository.CreateManagerAsync(manager);
        }

        public async Task<bool> UpdateManagerAsync(int id, ManagerModel manager)
        {
            return await _managerRepository.UpdateManagerAsync(id, manager);
        }

        public async Task<bool> DeleteManagerAsync(int id)
        {
            return await _managerRepository.DeleteManagerAsync(id);
        }
    }
}
