
using GoGo.Core.Dtos;
using GoGo.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Application.Features.Courses.Queries
{
    // Query này yêu cầu một danh sách CourseDto
    public record GetAllCoursesQuery() : IRequest<IEnumerable<CourseDto>>;

    public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, IEnumerable<CourseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCoursesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CourseDto>> Handle(GetAllCoursesQuery query, CancellationToken cancellationToken)
        {
            var courses = await _unitOfWork.Courses.GetCourseAllAsync();

            // Ánh xạ từ Domain Entity (Course) sang DTO (CourseDto)
            return courses.Select(course => new CourseDto(
                course.Id,
                course.Name,
                course.ThumbnailUrl,
                course.Price,
                course.SkillLevel
            ));
        }
    }
}
