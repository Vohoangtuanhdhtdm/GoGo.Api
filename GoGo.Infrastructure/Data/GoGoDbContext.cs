using GoGo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Infrastructure.Data
{
    public class GoGoDbContext(DbContextOptions<GoGoDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Enrollment> EnrollCourses { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Module> Module { get; set; }
        public DbSet<Lesson> Lesson { get; set; }
        public DbSet<LessonCompletion> LessonCompletion { get; set; }
     
    }
}
