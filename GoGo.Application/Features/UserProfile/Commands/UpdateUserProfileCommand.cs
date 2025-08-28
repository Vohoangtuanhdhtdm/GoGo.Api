using GoGo.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Application.Features.UserProfile.Commands
{
    public record UpdateUserProfileCommand(Guid userId, string fullName, string AvatarUrl) : IRequest;

    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateUserProfileCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserProfiles.GetUserProfileByIdAsync(request.userId);
            if (user == null)
            {
                throw new Exception($"User with Id {request.userId} not found.");
            }
            user.UpdateProfile(request.fullName, request.AvatarUrl);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
