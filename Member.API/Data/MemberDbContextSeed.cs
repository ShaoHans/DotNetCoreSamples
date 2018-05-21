using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Member.API.Data
{
    public class MemberDbContextSeed
    {
        public async Task SeedAsync(MemberDbContext context, IServiceProvider service)
        {
            using (var scope = service.CreateScope())
            {
                context.Database.Migrate();

                if (!await context.Users.AnyAsync())
                {
                    context.Users.Add(new Data.Entities.User { Name = "shz", Phone = "13914561236" });
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
