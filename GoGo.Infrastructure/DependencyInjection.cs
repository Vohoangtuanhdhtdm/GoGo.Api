using GoGo.Core.Interfaces;
using GoGo.Infrastructure.Data;
using GoGo.Infrastructure.Repositories;
using GoGo.Infrastructure.Uow;
using Microsoft.EntityFrameworkCore;
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
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            services.AddDbContext<GoGoDbContext>(options =>
            {
                options.UseSqlServer("Server = MSI\\SQLEXPRESS; Database = GoGoDatabase; Trusted_Connection = True; TrustServerCertificate = true; MultipleActiveResultSets = true");
            });

            // Chỉ cần đăng ký IUnitOfWork. 
            // Nó sẽ chịu trách nhiệm cung cấp tất cả các repository cần thiết.
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

    }
}

