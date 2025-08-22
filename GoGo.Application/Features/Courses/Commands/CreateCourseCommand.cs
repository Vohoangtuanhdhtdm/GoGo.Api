using GoGo.Core.Entities;
using GoGo.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace GoGo.Application.Features.Courses.Commands
{
   
    public record CreateCourseCommand(
    string Name,
    string Description,
    string ThumbnailUrl,
    decimal Price,
    string SkillLevel) : IRequest<Guid>;

    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        // Inject Unit of Work để có thể truy cập vào các Repository
        public CreateCourseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Phương thức Handle chứa toàn bộ logic nghiệp vụ cho việc tạo khóa học
        public async Task<Guid> Handle(CreateCourseCommand command, CancellationToken cancellationToken)
        {
            // 1. Dùng constructor của Domain Entity để tạo một đối tượng Course mới,
            //    đảm bảo các quy tắc nghiệp vụ trong constructor được tuân thủ.
            var course = new Course(
                command.Name,
                command.Description,
                command.ThumbnailUrl,
                command.Price,
                command.SkillLevel
            );

            // 2. Dùng Repository để thêm đối tượng vào DbContext
            await _unitOfWork.Courses.AddCourseAsync(course);

            // 3. Dùng Unit of Work để commit transaction
            await _unitOfWork.SaveChangesAsync();

            // 4. Trả về Id của khóa học vừa được tạo
            return course.Id;
        }
    }
}
