using GoGo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Core.Interfaces
{
    public interface ILessonRepository
    {
        Task<IEnumerable<Lesson>> GetAllLesson();
        Task<Lesson> GetLessonById(Guid LessonId);
        Task<Lesson> UpdateLessonAsync(Lesson Lesson);
        Task<Lesson> AddLessonAsync(Lesson Lesson);
        Task<bool> DeleteLessonAsync(Guid LessonId);
    }
}
