using GoGo.Core.Interfaces;
using GoGo.Infrastructure.Data;
using GoGo.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Infrastructure.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GoGoDbContext _context;
        public ICourseRepository Courses { get; private set; }
        public IUserProfileRepository UserProfiles { get; private set; }
        public IEnrollmentRepository Enrollments { get; private set; }
        public IModuleRepository Modules { get; private set; }

        public UnitOfWork(GoGoDbContext context)
        {
            _context = context;
            Courses = new CourseRepository(_context);
            UserProfiles = new UserProfileRepository(_context);
            Enrollments = new EnrollmentRepository(_context);
            Modules = new ModuleRepository(_context);

        }
        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
