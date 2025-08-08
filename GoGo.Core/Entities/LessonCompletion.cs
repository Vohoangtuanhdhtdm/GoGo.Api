using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Core.Entities
{
    public class LessonCompletion
    {
        // Thuộc tính
        
        public Guid Id { get; set; }
        public DateTime? CompletedAt { get; set; }

        // Quan hệ
        public Guid? EnrollmentId { get; set; }
        public Guid? LessonId { get; set; }   


        // Navigation
        public Enrollment? Enrollment { get; set; }
        public Lesson? Lesson { get; set; }
        
    }
}
