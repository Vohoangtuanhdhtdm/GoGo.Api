using System;
using System.Collections.Generic;
using System.Linq;

namespace GoGo.Core.Entities
{
    public class Enrollment
    {
        // Dùng một list private để toàn quyền kiểm soát việc thêm/xóa
        private readonly List<LessonCompletion> _lessonCompletions = new();

        #region Thông tin cơ bản
        public Guid Id { get; private set; }
        public DateTime EnrollmentDate { get; private set; }
        public int ProgressPercentage { get; private set; }
        public int TotalLessons { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        #endregion

        #region Quan hệ
        public Guid UserId { get; private set; }
        public Guid CoursesId { get; private set; }
        #endregion

        #region Navigation Properties
        // Chỉ cho phép bên ngoài đọc danh sách, không cho phép sửa đổi trực tiếp
        public IReadOnlyCollection<LessonCompletion> LessonCompletions => _lessonCompletions.AsReadOnly();
        #endregion

        private Enrollment() { } // For EF Core

        public Enrollment(Guid userId, Guid courseId, int totalLessons)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            CoursesId = courseId;
            TotalLessons = totalLessons;
            EnrollmentDate = DateTime.UtcNow;
            ProgressPercentage = 0;
        }

        #region Các phương thức hành vi
        public void MarkLessonAsCompleted(Guid lessonId)
        {
            // 1. Kiểm tra quy tắc nghiệp vụ: không hoàn thành một bài học hai lần
            if (_lessonCompletions.Any(lc => lc.LessonId == lessonId))
                return;

            // 2. Tạo ra sự kiện mới và thay đổi trạng thái bên trong Aggregate
            var newCompletion = new LessonCompletion(this.Id, lessonId);
            _lessonCompletions.Add(newCompletion);

            // 3. Đảm bảo tính nhất quán bằng cách tính toán lại tiến độ
            RecalculateProgress();
        }

        private void RecalculateProgress()
        {
            if (TotalLessons > 0)
            {
                ProgressPercentage = (int)Math.Round((double)_lessonCompletions.Count / TotalLessons * 100);
            }
            else
            {
                ProgressPercentage = 100;
            }

            if (ProgressPercentage >= 100)
            {
                CompletedAt = DateTime.UtcNow;
            }
        }
        #endregion
    }
}
