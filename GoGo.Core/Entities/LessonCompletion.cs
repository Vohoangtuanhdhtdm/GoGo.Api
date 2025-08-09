using System;

namespace GoGo.Core.Entities
{
    public class LessonCompletion
    {
        #region Thông tin cơ bản
        public Guid Id { get; private set; }
        public DateTime CompletedAt { get; private set; }
        #endregion

        #region Quan hệ
        public Guid EnrollmentId { get; private set; }
        public Guid LessonId { get; private set; }
        #endregion

        private LessonCompletion() { } // Dành cho EF Core

        public LessonCompletion(Guid enrollmentId, Guid lessonId)
        {
            Id = Guid.NewGuid();
            EnrollmentId = enrollmentId;
            LessonId = lessonId;
            CompletedAt = DateTime.UtcNow;
        }
    }
}


// Bắt buộc phải thêm private LessonCompletion() {} cho LessonCompletion
// vì bạn đã tạo một constructor khác,làm mất đi cái "miễn phí" kia.