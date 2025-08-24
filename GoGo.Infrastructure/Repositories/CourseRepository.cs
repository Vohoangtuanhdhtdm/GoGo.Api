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
        public async Task<Course?> GetCourseByIdAsync(Guid id)
        {
            // Phải Include các thực thể con để tải toàn bộ Aggregate
            return await _context.Courses
                .Include(c => c.Modules)
                .ThenInclude(m => m.Lessons)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<IEnumerable<Course>> GetCourseAllAsync()
        {
            return await _context.Courses.ToListAsync();
        }
        public async Task AddCourseAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
        }
     
        public async Task DeleteCourseAsync(Guid id)
        {
           
            var courseToDelete = await _context.Courses
                .Include(c => c.Modules) 
                .FirstOrDefaultAsync(c => c.Id == id);

            if (courseToDelete != null)
            {
                _context.Courses.Remove(courseToDelete);
            }
        }

        
    }
}
