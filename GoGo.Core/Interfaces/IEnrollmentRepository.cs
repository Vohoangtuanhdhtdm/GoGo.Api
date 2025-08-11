using GoGo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Core.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<IEnumerable<Enrollment>> GetEnrollmentByCourseIdAsync(Guid courseId);
        Task<Enrollment?> GetByEnrollmentIdAsync(Guid id);
        Task AddEnrollmentAsync(Enrollment enrollment);
        Task UpdateEnrollmentAsync(Enrollment enrollment);
    }
}
