using GoGo.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoGo.Application.Features.Courses.Commands
{
    // IRequest nghĩa là command này không trả về dữ liệu gì sau khi thực thi
    public record UpdateCourseCommand(
        Guid Id,
        string Name,
        string Description,
        string SkillLevel,
        string ThumbnailUrl,
        decimal? Price = null,
        decimal? PriceSale = null
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
                throw new Exception($"Course with Id {command.Id} not found.");
            }

            // 3. Chỉnh sửa entity thông qua method domain
            course.Update(
                command.Name,
                command.Description,
                command.SkillLevel,
                command.ThumbnailUrl,
                command.Price,
                command.PriceSale
            );
            // repository update
           // await _unitOfWork.Courses.UpdateCourseAsync(course);

            // 4. Lưu lại thay đổi (EF Core tự track entity)
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
