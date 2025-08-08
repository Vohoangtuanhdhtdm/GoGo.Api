using GoGo.Core.Entities;
using GoGo.Core.Interfaces;
using GoGo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Infrastructure.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly GoGoDbContext _goGoDbContext;

        public ModuleRepository(GoGoDbContext goGoDbContext)
        {
            _goGoDbContext = goGoDbContext;
        }


        public async Task<Module> AddModuleAsync(Module Module)
        {
            await _goGoDbContext.Module.AddAsync(Module);
            await _goGoDbContext.SaveChangesAsync();
            return Module;
        }

        public async Task<bool> DeleteModuleAsync(Guid ModuleId)
        {
            var ModuleToDelete = await _goGoDbContext.Module.FindAsync(ModuleId);

            if (ModuleToDelete == null)
            {
                return false;
            }
            _goGoDbContext.Module.Remove(ModuleToDelete);
            await _goGoDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Module>> GetAllModule()
        {
            return await _goGoDbContext.Module.ToListAsync();
        }

        public async Task<Module?> GetModuleById(Guid ModuleId)
        {
            // trả về null nếu không tìm thấy
            return await _goGoDbContext.Module.FindAsync(ModuleId);
        }

        public async Task<Module?> UpdateModuleAsync(Module module)
        {
            var existingModule = await _goGoDbContext.Module.FindAsync(module.Id);
            if (existingModule == null)
            {
                return null;
            }
            existingModule.Title = module.Title;
            existingModule.Description = module.Description;
            existingModule.DisplayOrder = module.DisplayOrder;
            existingModule.Duration = module.Duration;
            await _goGoDbContext.SaveChangesAsync();
            return existingModule;
        }
    }
}
