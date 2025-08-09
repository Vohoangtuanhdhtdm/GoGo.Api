using GoGo.Core.Entities;
using GoGo.Core.Interfaces;
using GoGo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GoGo.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly GoGoDbContext _context;

    public UserRepository(GoGoDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id)
    {
        var userToDelete = await _context.Users.FindAsync(id);
        if (userToDelete != null)
        {
            _context.Users.Remove(userToDelete);
        }
    }
}