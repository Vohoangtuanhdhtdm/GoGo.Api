using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Core.Dtos
{
    public record LessonDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public string VideoUrl { get; set; } = default!;
        public string? Content { get; set; }
        public int Duration { get; set; }
        public int DisplayOrder { get; set; }
        public Guid ModuleId { get; set; }
    }
}
