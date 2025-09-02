using GoGo.Core.Entities;
using GoGo.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

public static class DataSeeder
{
    public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
    {
        // Lấy các dịch vụ cần thiết
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser<Guid>>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>(); 

        // Danh sách các vai trò cần có
        string[] roleNames = { "Admin", "Member" };

        // Tạo các vai trò nếu chúng chưa tồn tại
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                // Tạo vai trò mới
                await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
            }
        }

        // Tạo người dùng Admin mặc định nếu chưa có
        var adminUser = await userManager.FindByEmailAsync("admin@gogo.com");
        if (adminUser == null)
        {
            var newAdminUser = new IdentityUser<Guid>
            {
                UserName = "admin@gogo.com",
                Email = "admin@gogo.com",
                EmailConfirmed = true // Bỏ qua bước xác thực email cho admin
            };
            var result = await userManager.CreateAsync(newAdminUser, "Admin@123"); // Đặt mật khẩu an toàn hơn

            if (result.Succeeded)
            {
                // Gán vai trò "Admin" cho người dùng vừa tạo
                await userManager.AddToRoleAsync(newAdminUser, "Admin");

                // Tạo hồ sơ người dùng tương ứng trong UserProfile
                var adminProfile = new UserProfile(
                    newAdminUser.Id,
                    "Administrator", // Tên đầy đủ cho Admin
                    newAdminUser.Email
                );
                await unitOfWork.UserProfiles.AddUserProfileAsync(adminProfile);
                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}