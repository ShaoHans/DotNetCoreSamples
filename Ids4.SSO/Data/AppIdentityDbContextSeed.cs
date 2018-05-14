using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Ids4.SSO.Data
{
    public class AppIdentityDbContextSeed
    {
        public async Task SeedAsync(AppIdentityDbContext context, IServiceProvider service)
        {
            if(!context.Roles.Any())
            {
                RoleManager<AppRole> roleManager = service.GetRequiredService<RoleManager<AppRole>>();
                AppRole administratorRole = new AppRole
                {
                    Name = "Administrators",
                    NormalizedName = "系统管理员"
                };
                await roleManager.CreateAsync(administratorRole);

                AppRole managerRole = new AppRole
                {
                    Name = "Manager",
                    NormalizedName = "经理"
                };
                await roleManager.CreateAsync(managerRole);

            }

            if(!context.Users.Any())
            {
                UserManager<AppUser> userManager = service.GetRequiredService<UserManager<AppUser>>();
                AppUser admin = new AppUser
                {
                    UserName = "admin",
                    Email = "123456@qq.com",
                    NormalizedUserName = "admin",
                    Avatar = "https://www.wzfzl.cn/uploads/allimg/170314/16024554R-0.jpg",
                    PhoneNumber = "15088889999",
                    SecurityStamp = "xhsnhz"
                };

                await userManager.CreateAsync(admin, "123456");
                await userManager.AddToRolesAsync(admin, new List<string> { "Administrators", "Manager" });
            }
        }
    }
}
