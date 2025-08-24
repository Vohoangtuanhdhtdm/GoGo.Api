using GoGo.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Application.Features.Courses.Module.Commands
{
    public record UpdateModuleCommand (Guid moduleId, string newTitle, string newDescription) : IRequest;

    public class UpdateModuleCommandHandler : IRequestHandler<UpdateModuleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateModuleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(UpdateModuleCommand request, CancellationToken cancellationToken)
        {
            var module = await _unitOfWork.Modules.GetModuleByIdAsync(request.moduleId, cancellationToken);
            if (module == null)
            {
                throw new Exception($"Module with Id {request.moduleId} not found.");
            }
            module.UpdateModuleDetails(request.newTitle, request.newDescription);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
