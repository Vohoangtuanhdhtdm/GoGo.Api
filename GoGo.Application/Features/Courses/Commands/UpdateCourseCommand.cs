using GoGo.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Application.Features.Courses.Commands
{
    // IRequest (hoặc IRequest<Unit>) nghĩa là command này không trả về dữ liệu gì sau khi thực thi
    public record UpdateCourseCommand(
        Guid Id,
        string Name,
        string Description,
        string SkillLevel
    ) : IRequest;

    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCourseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateCourseCommand command, CancellationToken cancellationToken)
        {
            // 1. Tải Aggregate Root 'Course'
            var course = await _unitOfWork.Courses.GetCourseByIdAsync(command.Id);

            // 2. Kiểm tra sự tồn tại
            if (course == null)
            {
                // Trong một ứng dụng thực tế, bạn nên ném ra một Exception tùy chỉnh (ví dụ: NotFoundException)
                // và xử lý nó ở một middleware để trả về lỗi 404.
                throw new Exception($"Course with Id {command.Id} not found.");
            }

            // 3. Gọi phương thức hành vi của Domain Entity
            course.UpdateDetails(command.Name, command.Description, command.SkillLevel);

            // 4. Lưu lại thay đổi
            await _unitOfWork.SaveChangesAsync();
        }
    }

}
