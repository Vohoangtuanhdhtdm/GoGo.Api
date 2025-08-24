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
    public class LessonRepository : ILessonRepository
    {
        private readonly GoGoDbContext _context;
        public LessonRepository(GoGoDbContext context)
        {
            _context = context;
        }
        public async Task AddLessonAsync(Lesson lesson)
        {
            await _context.Lesson.AddAsync(lesson);
        }

        public async Task DeleteLessonAsync(Guid id)
        {
            var lesson = await _context.Lesson
                .FirstOrDefaultAsync(x => x.Id == id);
            if (lesson != null)
            {
                _context.Lesson.Remove(lesson);
            }
        }

        public async Task<IEnumerable<Lesson>> GetAllLessonOfModule(Guid moduleId)
        {
            var lesson = await _context.Lesson
                .Where(x => x.ModuleId == moduleId)
                .OrderBy(m => m.DisplayOrder)
                .ToListAsync();
            return lesson;
        }

        public async Task<Lesson?> GetLessonByIdAsync(Guid lessonId)
        {
            var lesson = await _context.Lesson.FirstOrDefaultAsync(m => m.Id == lessonId);
            if (lesson == null)
            {
                return null;
            }
            return lesson;
        }
    }
}
