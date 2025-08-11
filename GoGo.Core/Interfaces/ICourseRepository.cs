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
        Task<IEnumerable<Course>> GetCourseAllAsync();
        Task<Course?> GetCourseByIdAsync(Guid id);  // Trả về nullable để thể hiện có thể không tìm thấy
        Task AddCourseAsync(Course course);
        Task UpdateCourseAsync(Course course);
        Task DeleteCourseAsync(Guid id);
    }
}
