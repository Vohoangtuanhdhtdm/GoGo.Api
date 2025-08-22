using GoGo.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Application.Features.Courses.Module.Commands
{
    // ---- ĐỊNH NGHĨA KẾT QUẢ TRẢ VỀ ----
    // Một record cho đối tượng trả về, giúp việc truyền dữ liệu rõ ràng hơn
    public record CreateModuleResponse(Guid id,string Title, string Description);
    public record CreateModuleCommand(
        string Title,
        string Description, Guid CourseId
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
            var course = await _unitOfWork.Courses.GetCourseByIdAsync(request.CourseId);
            if (course == null)
            {
                throw new InvalidOperationException($"Course with ID {request.CourseId} not found.");
            }
            course.AddModule(request.Title, request.Description);
            await _unitOfWork.SaveChangesAsync();
            var newModule = course.Modules.Last();
            return new CreateModuleResponse(newModule.Id, newModule.Title, newModule.Description);
        }
    }
}
