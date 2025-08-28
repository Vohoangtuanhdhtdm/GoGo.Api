using GoGo.Core.Dtos;
using GoGo.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Application.Features.UserProfile.Queries
{
    public record GetUserProfileByIdQuery(Guid userId) : IRequest<UserDto?>;

    public class GetUserProfileByIdQueryHandler : IRequestHandler<GetUserProfileByIdQuery, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetUserProfileByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<UserDto?> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserProfiles.GetUserProfileByIdAsync(request.userId);

            if (user == null)
            {
                return null;
            }
            return new UserDto(
                user.FullName,
                user.Email,
                user.AvatarUrl,
                user.JoinedAt
           );
        }
    }
}
