using GoGo.Core.Dtos;
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
        private readonly GoGoDbContext _context;

        public ModuleRepository(GoGoDbContext context)
        {
            _context = context;
        }

        public async Task AddModuleAsync(Module module)
        {
            await _context.Module.AddAsync(module);
        }

        public async Task<IEnumerable<ModuleDto>> GetModuleByCourseIdAsync(Guid courseId, CancellationToken cancellationToken)
        {
            return await _context.Module 
                 .Where(m => m.CoursesId == courseId)
                 .OrderBy(m => m.DisplayOrder)
                 .Select(m => new ModuleDto(m.Id, m.Title, m.Description, m.DisplayOrder))
                 .ToListAsync(cancellationToken);
        }
    }
}
