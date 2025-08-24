using GoGo.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Application.Features.Courses.Commands
{
    public record DeleteCourseCommand(Guid Id) : IRequest;

    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCourseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteCourseCommand command, CancellationToken cancellationToken)
        {
        
            await _unitOfWork.Courses.DeleteCourseAsync(command.Id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
