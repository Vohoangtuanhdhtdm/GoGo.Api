using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Core.Dtos
{
    public record ModuleDto(Guid Id, string Title, string? Description, int DisplayOrder);
}
