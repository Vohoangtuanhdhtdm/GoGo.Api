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
        private readonly GoGoDbContext _goGoDbContext;

        public CourseRepository(GoGoDbContext goGoDbContext)
        {
            _goGoDbContext = goGoDbContext;
        }


        public async Task<Course> AddCourseAsync(Course Course)
        {
            await _goGoDbContext.Courses.AddAsync(Course);
            await _goGoDbContext.SaveChangesAsync();
            return Course;
        }

        public async Task<bool> DeleteCourseAsync(Guid CourseId)
        {
            var CourseToDelete = await _goGoDbContext.Courses.FindAsync(CourseId);

            if (CourseToDelete == null)
            {
                return false;
            }
            _goGoDbContext.Courses.Remove(CourseToDelete);
            await _goGoDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Course>> GetAllCourse()
        {
            return await _goGoDbContext.Courses.ToListAsync();
        }

        public async Task<Course?> GetCourseById(Guid CourseId)
        {
            // trả về null nếu không tìm thấy
            return await _goGoDbContext.Courses.FindAsync(CourseId);
        }

        public async Task<Course?> UpdateCourseAsync(Course course)
        {
            var existingCourse = await _goGoDbContext.Courses.FindAsync(course.Id);
            if (existingCourse == null)
            {
                return null;
            }
            existingCourse.Name = course.Name;
            existingCourse.Description = course.Description;
            existingCourse.ThumbnailUrl = course.ThumbnailUrl;
            existingCourse.Status = course.Status;
            existingCourse.Price = course.Price;
            existingCourse.PriceSale = course.PriceSale;
            existingCourse.SkillLevel = course.SkillLevel;

            
            existingCourse.UpdatedAt = DateTime.UtcNow;
            await _goGoDbContext.SaveChangesAsync();
            return existingCourse;
        }
    }
}
