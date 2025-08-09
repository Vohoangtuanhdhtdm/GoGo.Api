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

        private Lesson() { } // Dành cho EF Core

        internal Lesson(
            string title,
            string videoUrl,
            string? content,
            int duration,
            int displayOrder,
            Guid moduleId)
        {
            Id = Guid.NewGuid();
            Title = title;
            VideoUrl = videoUrl;
            Content = content;
            Duration = duration;
            DisplayOrder = displayOrder;
            ModuleId = moduleId;
        }
    }
}
