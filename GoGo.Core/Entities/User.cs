using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Core.Entities
{
    public class User
    {
        // Thuộc tính
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? AvatarUrl { get; set; }
        public string? PasswordHash { get; set; }
        public DateTime JoinedAt { get; set; }

        // Quan hệ
        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
