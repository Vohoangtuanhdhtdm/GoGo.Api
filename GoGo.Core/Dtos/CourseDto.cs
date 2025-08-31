using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Core.Dtos
{
    public record CourseDto(
        Guid Id,
        string Name,
        string Description,
        string ThumbnailUrl,
        decimal Price,
        string SkillLevel
    );
}
