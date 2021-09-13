using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISDB.Model;
using ISDB.Common;

namespace ISDBService.Common
{
    public interface IUserService
    {
        Task<List<UserModel>> GetUserAsync(Sorter sorter, Pager pager, UserFilter userFilter);
        Task<UserModel> GetUserByIdAsync(int id);
        Task<bool> CreateUserAsync(UserModel user);
        Task<bool> UpdateUserAsync(int id, UserModel user);
        Task<bool> DeleteUserAsync(int id);
    }
}
