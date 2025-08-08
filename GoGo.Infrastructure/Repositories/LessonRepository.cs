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
    public class LessonRepository:ILessonRepository
    {
        private readonly GoGoDbContext _goGoDbContext;

        public LessonRepository(GoGoDbContext goGoDbContext)
        {
            _goGoDbContext = goGoDbContext;
        }


        public async Task<Lesson> AddLessonAsync(Lesson Lesson)
        {
            await _goGoDbContext.Lesson.AddAsync(Lesson);
            await _goGoDbContext.SaveChangesAsync();
            return Lesson;
        }

        public async Task<bool> DeleteLessonAsync(Guid LessonId)
        {
            var LessonToDelete = await _goGoDbContext.Lesson.FindAsync(LessonId);

            if (LessonToDelete == null)
            {
                return false;
            }
            _goGoDbContext.Lesson.Remove(LessonToDelete);
            await _goGoDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Lesson>> GetAllLesson()
        {
            return await _goGoDbContext.Lesson.ToListAsync();
        }

        public async Task<Lesson?> GetLessonById(Guid LessonId)
        {
            // trả về null nếu không tìm thấy
            return await _goGoDbContext.Lesson.FindAsync(LessonId);
        }

        public async Task<Lesson?> UpdateLessonAsync(Lesson lesson)
        {
            var existingLesson = await _goGoDbContext.Lesson.FindAsync(lesson.Id);
            if (existingLesson == null)
            {
                return null;
            }
            existingLesson.Title = lesson.Title;
            existingLesson.Description = lesson.Description;
            existingLesson.VideoUrl = lesson.VideoUrl;
            existingLesson.Content = lesson.Content;

            await _goGoDbContext.SaveChangesAsync();
            return existingLesson;
        }
    }
}
