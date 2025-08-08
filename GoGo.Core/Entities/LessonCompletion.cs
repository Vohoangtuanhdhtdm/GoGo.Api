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
        // Shared Primary Key: Id này vừa là PK, vừa là FK đến Lessons
        public Guid Id { get; set; }
        public DateTime? CompletedAt { get; set; }

        // Quan hệ
        public Guid? EnrollmentId { get; set; }
       

        // Navigation
        public Enrollment? Enrollment { get; set; }
        public Lesson? Lessons { get; set; }
        
    }
}
