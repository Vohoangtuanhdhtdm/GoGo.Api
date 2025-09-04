using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GoGo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<IdentityUser<Guid>> _userManager;

        public AdminController(UserManager<IdentityUser<Guid>> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost("users/{userId}/assign-admin")]
        public async Task<IActionResult> AssignAdminRole(Guid userId)
        {
            // 1. Tìm người dùng mục tiêu
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // 2. Kiểm tra xem người dùng đã là Admin chưa
            var isAlreadyAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            if (isAlreadyAdmin)
            {
                return BadRequest("User is already an Admin.");
            }

            // 3. Gán vai trò Admin
            var result = await _userManager.AddToRoleAsync(user, "Admin");

            if (result.Succeeded)
            {
                return Ok($"User {user.Email} has been successfully assigned the Admin role.");
            }

            return BadRequest(result.Errors);
        }


        [HttpPost("users/{userId}/remove-admin")]
        public async Task<IActionResult> RemoveAdminRole(Guid userId)
        {
          
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // 2. Xóa vai trò Admin
            var result = await _userManager.RemoveFromRoleAsync(user, "Admin");

            if (result.Succeeded)
            {
                return Ok($"The Admin role has been successfully removed from user {user.Email}.");
            }

            return BadRequest(result.Errors);
        }
    }
}
