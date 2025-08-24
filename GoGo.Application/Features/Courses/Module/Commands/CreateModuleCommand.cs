using GoGo.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Application.Features.Courses.Module.Commands
{
    public record CreateModuleResponse(Guid Id, string Title, string Description);
    public record CreateModuleCommand(
        string Title,
        string Description,
        Guid CourseId
    ) : IRequest<CreateModuleResponse>;
    public class CreateModuleCommandHandler : IRequestHandler<CreateModuleCommand, CreateModuleResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateModuleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CreateModuleResponse> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
        {
            // Lấy course từ repository
            var course = await _unitOfWork.Courses.GetCourseByIdAsync(request.CourseId);
            if (course == null)
            {
                throw new InvalidOperationException($"Course with ID {request.CourseId} not found.");
            }

            // Thêm module vào course (domain logic)
            var newModule = course.AddModule(request.Title, request.Description);

            // Thêm module vào Db thông qua repository
            await _unitOfWork.Modules.AddModuleAsync(newModule);

            // Commit tất cả thay đổi 1 lần
            await _unitOfWork.SaveChangesAsync();

            return new CreateModuleResponse(newModule.Id, newModule.Title, newModule.Description);
        }
    }
}
