using GoGo.Core.Entities;
using GoGo.Core.Interfaces;
using GoGo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GoGoDbContext _goGoDbContext;

        public UserRepository(GoGoDbContext goGoDbContext) 
       {
            _goGoDbContext = goGoDbContext;
        }

      
        public async Task<User> AddUserAsync(User user)
        {
            await _goGoDbContext.Users.AddAsync(user);
            await _goGoDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var userToDelete = await _goGoDbContext.Users.FindAsync(userId);
          
            if (userToDelete == null)
            {
                return false;
            }
            _goGoDbContext.Users.Remove(userToDelete);
            await _goGoDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            return await _goGoDbContext.Users.ToListAsync();
        }

        public async Task<User?> GetUserById(Guid userId)
        {
            // trả về null nếu không tìm thấy
            return await _goGoDbContext.Users.FindAsync(userId);
        }

        public async Task<User?> UpdateUserAsync(User user)
        {
            var existingUser = await _goGoDbContext.Users.FindAsync(user.Id);
            if (existingUser == null)
            {
                return null;
            }
            existingUser.FullName = user.FullName;
            existingUser.Email = user.Email;
            existingUser.AvatarUrl = user.AvatarUrl;
            await _goGoDbContext.SaveChangesAsync();
            return existingUser;
        }
    }
}
