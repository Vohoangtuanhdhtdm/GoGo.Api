using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Core.Dtos
{
    public record UserDto
    (
        string FullName,
        string Email,
        string? AvatarUrl,
        DateTime JoinedAt
    );
    public record UserDtoFull
    (
        Guid UserId,
        string FullName,
        string Email,
        string? AvatarUrl,
        DateTime JoinedAt
    );
}
