using GoGo.Core.Entities;
using GoGo.Core.Interfaces;
using GoGo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GoGo.Infrastructure.Repositories;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly GoGoDbContext _context;

    public UserProfileRepository(GoGoDbContext context)
    {
        _context = context;
    }

    public async Task<UserProfile?> GetUserProfileByIdAsync(Guid id)
    {
        return await _context.UserProfile.FindAsync(id);
    }

    public async Task<IEnumerable<UserProfile>> GetUserProfileAllAsync()
    {
        return await _context.UserProfile.ToListAsync();
    }

    public async Task AddUserProfileAsync(UserProfile UserProfile)
    {
        await _context.UserProfile.AddAsync(UserProfile);
    }

    public Task UpdateUserProfileAsync(UserProfile UserProfile)
    {
        _context.UserProfile.Update(UserProfile);
        return Task.CompletedTask;
    }

    public async Task DeleteUserProfileAsync(Guid id)
    {
        var UserProfileToDelete = await _context.UserProfile.FindAsync(id);
        if (UserProfileToDelete != null)
        {
            _context.UserProfile.Remove(UserProfileToDelete);
        }
    }
}