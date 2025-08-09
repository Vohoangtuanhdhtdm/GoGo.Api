using System;

namespace GoGo.Core.Entities
{
    public class User
    {
        #region Thông tin cơ bản
        public Guid Id { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string? AvatarUrl { get; private set; }
        public string PasswordHash { get; private set; }
        public DateTime JoinedAt { get; private set; }
        #endregion

        private User() { } // Dành cho EF Core

        public User(string fullName, string email, string passwordHash)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            Email = email; // Cần có logic kiểm tra định dạng email ở tầng Application/Domain Service
            PasswordHash = passwordHash;
            JoinedAt = DateTime.UtcNow;
        }

        #region Các phương thức hành vi
        public void UpdateProfile(string newFullName, string? newAvatarUrl)
        {
            FullName = newFullName;
            AvatarUrl = newAvatarUrl;
        }

        public void ChangePassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash;
        }
        #endregion
    }
}
