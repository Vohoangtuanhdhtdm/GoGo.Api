using GoGo.Core.Interfaces;
using GoGo.Infrastructure.Data;
using GoGo.Infrastructure.Repositories;
using GoGo.Infrastructure.Uow;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            // Cấu hình DbContext
            services.AddDbContext<GoGoDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            // THÊM LẠI khối AddIdentity() đầy đủ
            services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<GoGoDbContext>()
            .AddDefaultTokenProviders(); // Quan trọng: Cần cho việc reset password


            // Chỉ cần đăng ký IUnitOfWork. 
            // Nó sẽ chịu trách nhiệm cung cấp tất cả các repository cần thiết.
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

    }
}

