using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICourseRepository Courses { get; }
        IUserProfileRepository UserProfiles { get; }
        IEnrollmentRepository Enrollments { get; }
        IModuleRepository Modules { get; }
        ILessonRepository Lessons { get; }
        

        Task<int> SaveChangesAsync();
    }
}
