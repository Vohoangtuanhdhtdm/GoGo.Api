using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Core.Entities
{
    public class Module
    {
        // Thuộc tính
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required int DisplayOrder { get; set; }
        public int Duration { get; set; }

        // Quan hệ
        public Guid? CoursesId { get; set; }

        // Navigation
        public Course? Course { get; set; }
        public ICollection<Lesson>? Lessons { get; set; }
        
    }
}
