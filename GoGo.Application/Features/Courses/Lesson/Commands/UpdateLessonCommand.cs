using GoGo.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Application.Features.Courses.Lesson.Commands
{
    public record UpdateLessonCommand(Guid lessonId, string title,
       string description,
       string videoUrl,
       string? content,
       int displayOrder,
       int duration) : IRequest;

    public class UpdateLessonCommandHandler : IRequestHandler<UpdateLessonCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateLessonCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
        {
            var lesson = await _unitOfWork.Lessons.GetLessonByIdAsync(request.lessonId);
            if (lesson == null)
            {
                throw new Exception($"lesson with Id {request.lessonId} not found.");
            }
            lesson.UpdateLessonDetail
                (request.title, request.content, request.description, request.videoUrl, request.duration, request.displayOrder);
        
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
