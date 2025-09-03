using System;
using System.Collections.Generic;

namespace GoGo.Core.Entities
{
    public class Module
    {
        private readonly List<Lesson> _lessons = new();

        #region Thông tin cơ bản
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int DisplayOrder { get; private set; }
        #endregion

        #region Quan hệ
        public Guid CoursesId { get; private set; }
        #endregion

        #region Navigation Properties
        public IReadOnlyCollection<Lesson> Lessons => _lessons.AsReadOnly();
        #endregion

        private Module() { } // Dành cho EF Core

        // Constructor 'internal' để chỉ có thể được gọi từ bên trong cùng assembly (từ Course)
        internal Module(string title, string description, int displayOrder, Guid courseId)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            DisplayOrder = displayOrder;
            CoursesId = courseId;
        }
        public void UpdateModuleDetails(string newTitle, string newDescription)
        {
            Title = newTitle;
            Description = newDescription;
        }

        #region Các phương thức hành vi
        public Lesson AddLesson(
            string title,
            string? description,
            string videoUrl,
            string? content,
            int duration,
            int displayOrder,
            Guid moduleId
        )
        {
            var lesson = new Lesson(
                title,
                description,
                videoUrl,
                content,
                duration,
                displayOrder,
                moduleId
            );

            // nếu bạn có List<Lesson> trong Module
            _lessons.Add(lesson);

            return lesson;
        }

        #endregion
    }
}
