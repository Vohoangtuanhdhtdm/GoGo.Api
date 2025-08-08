using GoGo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Core.Interfaces
{
    public interface ILessonCompletionCompletionRepository
    {
        Task<IEnumerable<LessonCompletion>> GetAllLessonCompletion();
        Task<LessonCompletion> GetLessonCompletionById(Guid LessonCompletionId);
        Task<LessonCompletion> UpdateLessonCompletionAsync(LessonCompletion LessonCompletion);
        Task<LessonCompletion> AddLessonCompletionAsync(LessonCompletion LessonCompletion);
        Task<bool> DeleteLessonCompletionAsync(Guid LessonCompletionId);
    }
}
