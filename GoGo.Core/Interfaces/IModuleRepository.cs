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
        Task<IEnumerable<Module>> GetAllModule();
        Task<Module> GetModuleById(Guid ModuleId);
        Task<Module> UpdateModuleAsync(Module Module);
        Task<Module> AddModuleAsync(Module Module);
        Task<bool> DeleteModuleAsync(Guid ModuleId);
    }
}
