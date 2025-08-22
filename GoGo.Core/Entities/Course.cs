using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace GoGo.Core.Entities
{
    public class Course
    {
        private readonly List<Module> _modules = new();

       
        #region Thuộc tính chính
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ThumbnailUrl { get; private set; }
        public string Status { get; private set; }
        #endregion

        #region Thời gian
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        #endregion

        #region Thông tin học tập & giá cả
        public int Duration { get; private set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; private set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? PriceSale { get; private set; }

        public string SkillLevel { get; private set; }
        #endregion

        #region Navigation Properties
        public IReadOnlyCollection<Module> Modules => _modules.AsReadOnly();
        #endregion

        private Course() { } // Dành cho EF Core

        public Course(string name, string description, string thumbnailUrl, decimal price, string skillLevel)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            ThumbnailUrl = thumbnailUrl;
            Price = price > 0 ? price : 0;
            SkillLevel = skillLevel;
            Status = "Draft";
            CreatedAt = DateTime.UtcNow;
        }

        #region Các phương thức hành vi

        #region Hành vi Của Course
        public void Publish()
        {
            if (!_modules.Any(m => m.Lessons.Any()))
                throw new InvalidOperationException("Không thể xuất bản khóa học chưa có bài học.");

            Status = "Published";
            UpdatedAt = DateTime.UtcNow;
        }
        public void UpdateDetails(string name, string description, string skillLevel, string thumbnailUrl)
        {
            Name = name;
            Description = description;
            SkillLevel = skillLevel;
            ThumbnailUrl = thumbnailUrl;
            UpdatedAt = DateTime.UtcNow;
        }
        #endregion

        #region Hành vi đến Module
        public Module AddModule(string title, string? description)
        {
            var newModule = new Module(title, description, _modules.Count + 1, Id);
            _modules.Add(newModule);
            UpdatedAt = DateTime.UtcNow; // Thêm dòng này để cập nhật thời gian

            // Trả về module vừa được tạo
            return newModule;
        }
        public void UpdateModuleDetails(Guid moduleId, string newTitle, string? newDescription)
        {
            
            var moduleToUpdate = _modules.FirstOrDefault(m => m.Id == moduleId);

            if (moduleToUpdate == null)
            {
                throw new InvalidOperationException($"Module with ID {moduleId} not found in this course.");
            }

    
            moduleToUpdate.UpdateDetails(newTitle, newDescription);

            // 3. Cập nhật dấu thời gian của toàn bộ khóa học
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveModule(Guid moduleId)
        {
            var moduleToRemove = _modules.FirstOrDefault(m => m.Id == moduleId);
            if (moduleToRemove != null)
            {
                _modules.Remove(moduleToRemove);
                RecalculateDuration(); 
                UpdatedAt = DateTime.UtcNow;
            }
        }
        #endregion


        // Gọi sau khi thêm/xóa bài học trong Module
        public void RecalculateDuration()
        {
            Duration = _modules.SelectMany(m => m.Lessons).Sum(l => l.Duration);
        }
        #endregion
    }
}
