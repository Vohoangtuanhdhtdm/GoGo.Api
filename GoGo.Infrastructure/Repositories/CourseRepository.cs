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
    public class CourseRepository : ICourseRepository
    {
        private readonly GoGoDbContext _context;

        public CourseRepository(GoGoDbContext context) 
        { 
            _context = context;
            
        }
        public async Task<Course?> GetByIdAsync(Guid id)
        {
            // Phải Include các thực thể con để tải toàn bộ Aggregate
            return await _context.Courses
                .Include(c => c.Modules)
                .ThenInclude(m => m.Lessons)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            // Khi lấy danh sách, thường không cần tải chi tiết để tối ưu hiệu năng
            return await _context.Courses.ToListAsync();
        }
        public async Task AddAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
        }
        public Task UpdateAsync(Course course)
        {
            // EF Core Change Tracker sẽ tự xử lý các thay đổi
            _context.Courses.Update(course);
            return Task.CompletedTask;
        }
        public async Task DeleteAsync(Guid id)
        {
            var courseToDelete = await _context.Courses.FindAsync(id);
            if (courseToDelete != null)
            {
                _context.Courses.Remove(courseToDelete);
            }
        }
    }
}
