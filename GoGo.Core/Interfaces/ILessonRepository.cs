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
        Task AddLessonAsync(Lesson lesson);
        Task<IEnumerable<Lesson>> GetAllLessonOfModule(Guid moduleId);
        Task <Lesson?> GetLessonByIdAsync(Guid lessonId);
        Task DeleteLessonAsync(Guid id);
    }
}
