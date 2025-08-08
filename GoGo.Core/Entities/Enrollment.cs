using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Core.Entities
{
    public class Enrollment
    {
        // Thuộc tính
        public Guid Id { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int ProgressPercentage { get; set; }
        public int TotalLessons { get; set; }
        public DateTime? CompletedAt { get; set; }

        // Quan hệ
        public Guid? UserId { get; set; }
        public Guid? CoursesId { get; set; }

        // Navigation
        public User? User { get; set; }
        public Course? Course { get; set; }
        public ICollection<LessonCompletion>? LessonCompletions { get; set; }

    }
}
