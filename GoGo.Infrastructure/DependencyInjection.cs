using GoGo.Infrastructure.Data;
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
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection service)
        {
            service.AddDbContext<GoGoDbContext>(options =>
            {
                options.UseSqlServer("Server = MSI\\SQLEXPRESS; Database = GoGoDatabase; Trusted_Connection = True; TrustServerCertificate = true; MultipleActiveResultSets = true");
            });
            return service;
        }

    }
}

