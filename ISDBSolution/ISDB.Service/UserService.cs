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
 
        public class UserService : IUserService
        {
            IUserRepository _userRepository;

            public UserService(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<List<UserModel>> GetUserAsync(Sorter sorter, Pager pager, UserFilter userFilter)
            {
                return await _userRepository.GetUserAsync(sorter, pager, userFilter);

            }

            public async Task<UserModel> GetUserByIdAsync(int id)
            {
                return await _userRepository.GetUserByIdAsync(id);
            }

            public async Task<bool> CreateUserAsync(UserModel user)
            {
                return await _userRepository.CreateUserAsync(user);
            }

            public async Task<bool> UpdateUserAsync(int id, UserModel user)
            {
                return await _userRepository.UpdateUserAsync(id, user);
            }

            public async Task<bool> DeleteUserAsync(int id)
            {
                return await _userRepository.DeleteUserAsync(id);
            }
        }
}

