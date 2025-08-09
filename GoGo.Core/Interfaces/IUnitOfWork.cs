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
        IUserRepository Users { get; }
        IEnrollmentRepository Enrollments { get; }

        Task<int> SaveChangesAsync();
    }
}
