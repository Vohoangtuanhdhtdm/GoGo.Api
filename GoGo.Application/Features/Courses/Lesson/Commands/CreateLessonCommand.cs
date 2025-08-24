using GoGo.Core.Dtos;
using GoGo.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Application.Features.Courses.Lesson.Commands
{
    public record CreateLessonCommand(
       string title,
       string? description,
       string videoUrl,
       string? content,
       int duration,
       int displayOrder,
       Guid moduleId
   ) : IRequest<LessonDto>;

    public class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand, LessonDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateLessonCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LessonDto> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
        {
            var module = await _unitOfWork.Modules.GetModuleByIdAsync(request.moduleId,cancellationToken);
            if (module == null)
            {
                throw new InvalidOperationException($"Module with ID {request.moduleId} not found.");
            }
            var newLesson = module.AddLesson(request.title, request.description, request.videoUrl,
                request.content, request.duration, request.displayOrder, request.moduleId);

            await _unitOfWork.Lessons.AddLessonAsync(newLesson);
            await _unitOfWork.SaveChangesAsync();
            
            return new LessonDto
            {
                Id = newLesson.Id,
                Title = request.title,
                Description = request.description,
                VideoUrl = request.videoUrl,
                Content = request.content,
                DisplayOrder = request.displayOrder,
                ModuleId = request.moduleId,
            };
        }
    }
}
