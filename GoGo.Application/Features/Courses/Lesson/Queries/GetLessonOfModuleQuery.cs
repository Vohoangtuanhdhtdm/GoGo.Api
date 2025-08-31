using GoGo.Core.Dtos;
using GoGo.Core.Entities;
using GoGo.Core.Interfaces;
using MediatR;


namespace GoGo.Application.Features.Courses.Lesson.Queries
{
    public record GetLessonOfModuleQuery(Guid moduleId) : IRequest<IEnumerable<LessonDto>>;

    public class GetLessonOfModuleQueryHandler : IRequestHandler<GetLessonOfModuleQuery, IEnumerable<LessonDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetLessonOfModuleQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<LessonDto>> Handle(GetLessonOfModuleQuery request, CancellationToken cancellationToken)
        {
            var lesson = await _unitOfWork.Lessons.GetAllLessonOfModule(request.moduleId);
            if (lesson == null)
            {
                return Enumerable.Empty<LessonDto>();
            }

            // map sang LessonDto
            return lesson.Select(l => new LessonDto
            {
                Id = l.Id,
                Title = l.Title,
                VideoUrl = l.VideoUrl,
                Description = l.Description,
                Content = l.Content,
                Duration = l.Duration,
                DisplayOrder = l.DisplayOrder,
                ModuleId = l.ModuleId
            });
        }
    }
}
