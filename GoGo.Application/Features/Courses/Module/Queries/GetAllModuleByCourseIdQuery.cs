using GoGo.Core.Dtos;
using GoGo.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Application.Features.Courses.Module.Queries
{
    public record GetAllModuleByCourseIdQuery(Guid courseId) : IRequest<IEnumerable<ModuleDto>>;

    public class GetAllModuleByCourseIdQueryHandler : IRequestHandler<GetAllModuleByCourseIdQuery, IEnumerable<ModuleDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllModuleByCourseIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ModuleDto>> Handle(GetAllModuleByCourseIdQuery request, CancellationToken cancellationToken)
        {
            var module = await _unitOfWork.Modules.GetModuleByCourseIdAsync(request.courseId, cancellationToken);
            if (module == null)
            {
                return Enumerable.Empty<ModuleDto>();
            }
            return module;
        }
    }
}
