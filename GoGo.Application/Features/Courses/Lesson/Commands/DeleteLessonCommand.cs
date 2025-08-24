using GoGo.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Application.Features.Courses.Lesson.Commands
{
    public record DeleteLessonCommand(Guid lessonId) : IRequest;
    public class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteLessonCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Lessons.DeleteLessonAsync(request.lessonId);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
