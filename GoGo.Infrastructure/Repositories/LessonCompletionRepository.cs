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
    public class LessonCompletionRepository : ILessonCompletionCompletionRepository
    {
        private readonly GoGoDbContext _goGoDbContext;

        public LessonCompletionRepository(GoGoDbContext goGoDbContext)
        {
            _goGoDbContext = goGoDbContext;
        }


        public async Task<LessonCompletion> AddLessonCompletionAsync(LessonCompletion LessonCompletion)
        {
            await _goGoDbContext.LessonCompletion.AddAsync(LessonCompletion);
            await _goGoDbContext.SaveChangesAsync();
            return LessonCompletion;
        }

        public async Task<bool> DeleteLessonCompletionAsync(Guid LessonCompletionId)
        {
            var LessonCompletionToDelete = await _goGoDbContext.LessonCompletion.FindAsync(LessonCompletionId);

            if (LessonCompletionToDelete == null)
            {
                return false;
            }
            _goGoDbContext.LessonCompletion.Remove(LessonCompletionToDelete);
            await _goGoDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<LessonCompletion>> GetAllLessonCompletion()
        {
            return await _goGoDbContext.LessonCompletion.ToListAsync();
        }

        public async Task<LessonCompletion?> GetLessonCompletionById(Guid LessonCompletionId)
        {
            // trả về null nếu không tìm thấy
            return await _goGoDbContext.LessonCompletion.FindAsync(LessonCompletionId);
        }

        public async Task<LessonCompletion?> UpdateLessonCompletionAsync(LessonCompletion lessonCompletion)
        {
            var existingLessonCompletion = await _goGoDbContext.LessonCompletion.FindAsync(lessonCompletion.Id);
            if (existingLessonCompletion == null)
            {
                return null;
            }
            existingLessonCompletion.CompletedAt = lessonCompletion.CompletedAt;

            await _goGoDbContext.SaveChangesAsync();

            return existingLessonCompletion;
        }
    }
}
