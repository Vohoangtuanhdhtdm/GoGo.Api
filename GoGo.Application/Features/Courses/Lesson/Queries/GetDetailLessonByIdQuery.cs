using GoGo.Core.Dtos;
using GoGo.Core.Entities;
using GoGo.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Application.Features.Courses.Lesson.Queries
{
    public record GetDetailLessonByIdQuery(Guid lessonId) : IRequest<LessonDto>;

    public class GetDetailLessonByIdQueryHandler : IRequestHandler<GetDetailLessonByIdQuery, LessonDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetDetailLessonByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<LessonDto> Handle(GetDetailLessonByIdQuery request, CancellationToken cancellationToken)
        {
            var lesson = await _unitOfWork.Lessons.GetLessonByIdAsync(request.lessonId);
            if (lesson == null)
            {
                throw new Exception("Not fond");
            }
            return new LessonDto
            {
                Id = lesson.Id,
                Title = lesson.Title,
                Description = lesson.Description,
                Duration = lesson.Duration,
                Content = lesson.Content,
                VideoUrl = lesson.VideoUrl,
                ModuleId = lesson.ModuleId,
                DisplayOrder = lesson.DisplayOrder
            };
        }
    }
}
