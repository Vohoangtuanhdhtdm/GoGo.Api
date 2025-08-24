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
        public void AddLesson(string title, string videoUrl, int durationInSeconds, string? content = null)
        {
            var newLesson = new Lesson(title, videoUrl, content, durationInSeconds, _lessons.Count + 1, Id);
            _lessons.Add(newLesson);
        }
        #endregion
    }
}
