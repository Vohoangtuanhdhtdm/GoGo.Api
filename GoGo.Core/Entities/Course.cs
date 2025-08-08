using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Core.Entities
{
    public class Course
    {
        // Thuộc tính
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string ThumbnailUrl { get; set; }
        public required string Status { get; set; }
        public int? Duration { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceSale { get; set;}
        public string? SkillLevel { get; set; }

        // Quan hệ
        public ICollection<Enrollment>? Enrollments { get; set; }
        public ICollection<Module>? Modules { get; set; }
       
    }
}
