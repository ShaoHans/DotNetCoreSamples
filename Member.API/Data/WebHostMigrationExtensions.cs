using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Member.API.Data
{
    public static class WebHostMigrationExtensions
    {
        public static IWebHost MigrateDbContext<TContext>(this IWebHost host, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var context = service.GetRequiredService<TContext>();
                context.Database.Migrate();
                seeder(context, service);
            }
            return host;
        }
    }
}
