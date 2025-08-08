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
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly GoGoDbContext _goGoDbContext;

        public EnrollmentRepository(GoGoDbContext goGoDbContext)
        {
            _goGoDbContext = goGoDbContext;
        }


        public async Task<Enrollment> AddEnrollmentAsync(Enrollment Enrollment)
        {
            await _goGoDbContext.EnrollCourses.AddAsync(Enrollment);
            await _goGoDbContext.SaveChangesAsync();
            return Enrollment;
        }

        public async Task<bool> DeleteEnrollmentAsync(Guid EnrollmentId)
        {
            var EnrollmentToDelete = await _goGoDbContext.EnrollCourses.FindAsync(EnrollmentId);

            if (EnrollmentToDelete == null)
            {
                return false;
            }
            _goGoDbContext.EnrollCourses.Remove(EnrollmentToDelete);
            await _goGoDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Enrollment>> GetAllEnrollment()
        {
            return await _goGoDbContext.EnrollCourses.ToListAsync();
        }

        public async Task<Enrollment?> GetEnrollmentById(Guid EnrollmentId)
        {
            // trả về null nếu không tìm thấy
            return await _goGoDbContext.EnrollCourses.FindAsync(EnrollmentId);
        }

        public async Task<Enrollment?> UpdateEnrollmentAsync(Enrollment enrollment)
        {
            var existingEnrollment = await _goGoDbContext.EnrollCourses.FindAsync(enrollment.Id);
            if (existingEnrollment == null)
            {
                return null;
            }
            existingEnrollment.ProgressPercentage = enrollment.ProgressPercentage;
            existingEnrollment.CompletedAt = enrollment.CompletedAt;
            await _goGoDbContext.SaveChangesAsync();
            return existingEnrollment;
        }
    }
}
