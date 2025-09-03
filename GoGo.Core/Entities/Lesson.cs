using System;

namespace GoGo.Core.Entities
{
    public class Lesson
    {
        #region Thông tin cơ bản
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public string VideoUrl { get; private set; }
        public string? Content { get; private set; }
        public int Duration { get; private set; }
        public int DisplayOrder { get; private set; }
        #endregion

        #region Quan hệ
        public Guid ModuleId { get; private set; }
        #endregion

        private Lesson() { } 

        internal Lesson(
            string title,
             string? description,
            string videoUrl,
            string? content,
            int duration,
            int displayOrder,
            Guid moduleId)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            VideoUrl = videoUrl;
            Content = content;
            Duration = duration;
            DisplayOrder = displayOrder;
            ModuleId = moduleId;
        }

        #region Phương thức hành vi

        public void UpdateLessonDetail
        (
           string title,
           string? description,
           string videoUrl,
           string? content,
           int displayOrder,
           int duration
        )
        {
            Title = title;
            VideoUrl = videoUrl;
            Content = content;
            Duration = duration;
            Description = description;
            DisplayOrder = displayOrder;
        }

        #endregion
    }
}
