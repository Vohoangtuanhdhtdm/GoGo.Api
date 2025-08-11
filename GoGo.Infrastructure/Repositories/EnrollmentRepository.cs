using GoGo.Core.Entities;
using GoGo.Core.Interfaces;
using GoGo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GoGo.Infrastructure.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly GoGoDbContext _context;

    public EnrollmentRepository(GoGoDbContext context)
    {
        _context = context;
    }

    public async Task<Enrollment?> GetByEnrollmentIdAsync(Guid id)
    {
        // Luôn Include các thực thể con cần thiết cho logic nghiệp vụ
        return await _context.EnrollCourses
            .Include(e => e.LessonCompletions)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentByCourseIdAsync(Guid courseId)
    {
        return await _context.EnrollCourses
            .Where(e => e.CoursesId == courseId)
            .ToListAsync();
    }

    public async Task AddEnrollmentAsync(Enrollment enrollment)
    {
        await _context.EnrollCourses.AddAsync(enrollment);
    }

    public Task UpdateEnrollmentAsync(Enrollment enrollment)
    {
        _context.EnrollCourses.Update(enrollment);
        return Task.CompletedTask;
    }
}