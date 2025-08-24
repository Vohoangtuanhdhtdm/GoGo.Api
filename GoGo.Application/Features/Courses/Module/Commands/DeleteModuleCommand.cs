using GoGo.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Application.Features.Courses.Module.Commands
{
    public record DeleteModuleCommand(Guid moduleId) : IRequest;

    public class DeleteModuleCommandHandler : IRequestHandler<DeleteModuleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteModuleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteModuleCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Modules.DeleteModuleAsync(request.moduleId);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
