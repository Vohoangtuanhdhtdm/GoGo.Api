using GoGo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Core.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<IEnumerable<UserProfile>> GetUserProfileAllAsync();
        Task<UserProfile?> GetUserProfileByIdAsync(string id);
        Task AddUserProfileAsync(UserProfile userProfile);
        Task UpdateUserProfileAsync(UserProfile userProfile);
        Task DeleteUserProfileAsync(string id);
    }
}
