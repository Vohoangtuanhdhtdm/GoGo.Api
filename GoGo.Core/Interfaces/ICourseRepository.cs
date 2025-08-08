using GoGo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Core.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllCourse();
        Task<Course> GetCourseById(Guid CourseId);
        Task<Course> UpdateCourseAsync(Course Course);
        Task<Course> AddCourseAsync(Course Course);
        Task<bool> DeleteCourseAsync(Guid CourseId);
    }
}
