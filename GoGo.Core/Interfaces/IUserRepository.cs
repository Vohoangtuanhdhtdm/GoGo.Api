using GoGo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUser();
        Task<User> GetUserById(Guid UserId);
        Task<User> UpdateUserAsync(User User);     
        Task<User> AddUserAsync(User User);
        Task<bool> DeleteUserAsync(Guid UserId);
    }
}
