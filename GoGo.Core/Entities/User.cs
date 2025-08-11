using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoGo.Core.Entities
{
    // Id này sẽ là khóa chính và cũng là khóa ngoại
    // trỏ tới AspNetUsers.Id
    public class UserProfile
    {
        #region Thông tin cơ bản

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string? AvatarUrl { get; private set; }
      
        public DateTime JoinedAt { get; private set; }
        #endregion


        private UserProfile() { } // Dành cho EF Core

        public UserProfile(Guid id,string fullName, string email)
        {
            Id = id;
            FullName = fullName;
            Email = email; // Cần có logic kiểm tra định dạng email ở tầng Application/Domain Service
            JoinedAt = DateTime.UtcNow;
        }

        #region Các phương thức hành vi
        public void UpdateProfile(string newFullName, string? newAvatarUrl)
        {
            FullName = newFullName;
            AvatarUrl = newAvatarUrl;
        }

        
        #endregion
    }
}
