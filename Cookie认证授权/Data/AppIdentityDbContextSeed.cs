using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Cookie认证授权.Data
{
    public class AppIdentityDbContextSeed
    {
        public async Task SeedAsync(AppIdentityDbContext context, IServiceProvider service)
        {
            if(!context.Users.Any())
            {
                UserManager<AppUser> userManager = service.GetRequiredService<UserManager<AppUser>>();
                AppUser admin = new AppUser
                {
                    UserName = "admin",
                    Email = "123456@qq.com",
                    NormalizedUserName = "admin"
                };

                await userManager.CreateAsync(admin, "123456");
            }
        }
    }
}
