using GoGo.Core.Dtos;
using GoGo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Core.Interfaces
{
    public interface IModuleRepository
    {
        Task AddModuleAsync(Module module);
        Task<IEnumerable<ModuleDto>> GetModuleByCourseIdAsync(Guid courseId, CancellationToken cancellationToken);
        Task<Module?> GetModuleByIdAsync(Guid moduleId, CancellationToken cancellationToken);
        Task DeleteModuleAsync(Guid id);
    }
    

}
