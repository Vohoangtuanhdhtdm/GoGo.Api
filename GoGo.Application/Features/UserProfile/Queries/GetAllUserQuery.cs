using GoGo.Core.Dtos;
using GoGo.Core.Entities;
using GoGo.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Application.Features.UserProfile.Queries
{
    public record GetAllUserQuery():IRequest<IEnumerable<UserDtoFull>>;

    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, IEnumerable<UserDtoFull>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUserQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<UserDtoFull>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.UserProfiles.GetUserProfileAllAsync();
            return users.Select(user => new UserDtoFull(
                user.Id,
                user.FullName,
                user.Email,
                user.AvatarUrl,
                user.JoinedAt
                ));
        }
    }
}
