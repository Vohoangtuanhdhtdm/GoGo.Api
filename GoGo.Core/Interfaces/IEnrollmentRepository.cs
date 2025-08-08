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
        Task<IEnumerable<Enrollment>> GetAllEnrollment();
        Task<Enrollment> GetEnrollmentById(Guid EnrollmentId);
        Task<Enrollment> UpdateEnrollmentAsync(Enrollment Enrollment);
        Task<Enrollment> AddEnrollmentAsync(Enrollment Enrollment);
        Task<bool> DeleteEnrollmentAsync(Guid EnrollmentId);
    }
}
