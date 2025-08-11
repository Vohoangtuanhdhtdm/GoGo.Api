using GoGo.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace GoGo.Infrastructure.Data
{
    public class GoGoDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
    
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<Enrollment> EnrollCourses { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Module> Module { get; set; }
        public DbSet<Lesson> Lesson { get; set; }
        public DbSet<LessonCompletion> LessonCompletion { get; set; }

    public GoGoDbContext(DbContextOptions<GoGoDbContext> options) : base(options) { }

        

    }
}
