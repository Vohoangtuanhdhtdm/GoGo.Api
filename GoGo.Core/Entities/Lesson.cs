using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Core.Entities
{
    public class Lesson
    {
        // Thuộc tính
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public required string VideoUrl { get; set; }
        public string? Content { get; set; }

        // Quan hệ
        public Guid? ModuleId { get; set; }

        // Navigation
        public Module? Module { get; set; }
        public LessonCompletion? LessonCompletion { get; set; }
    }
}
