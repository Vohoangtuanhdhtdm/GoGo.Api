using GoGo.Application.Dto;
using GoGo.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Application.Features.Courses.Queries
{
    // CourseDto? nghĩa là kết quả có thể là null nếu không tìm thấy
    public record GetCourseByIdQuery(Guid Id) : IRequest<CourseDto?>;

    public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, CourseDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCourseByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CourseDto?> Handle(GetCourseByIdQuery query, CancellationToken cancellationToken)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(query.Id);

            if (course == null)
            {
                return null;
            }

            return new CourseDto(
                course.Id,
                course.Name,
                course.ThumbnailUrl,
                course.Price,
                course.SkillLevel
            );
        }
    }
}
