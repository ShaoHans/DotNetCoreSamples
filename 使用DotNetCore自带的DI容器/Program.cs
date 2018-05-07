using System;
using Microsoft.Extensions.DependencyInjection;

namespace 使用DotNetCore自带的DI容器
{
    class Program
    {
        static void Main(string[] args)
        {
            //IServiceCollection 负责注册
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IUserTransientService, UserTransientService>();
            serviceCollection.AddSingleton<IUserSingletonService, UserSingletonService>();
            serviceCollection.AddScoped<IUserScopeService, UserScopeService>();

            //IServiceProvider 负责提供实例
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            var userTransientService1 = serviceProvider.GetService<IUserTransientService>();
            var userTransientService2 = serviceProvider.GetService<IUserTransientService>();
            Console.WriteLine($"Transient:{userTransientService1.GetHashCode()==userTransientService2.GetHashCode()}");

            var userSingletonService1 = serviceProvider.GetService<IUserSingletonService>();
            var userSingletonService2 = serviceProvider.GetService<IUserSingletonService>();
            Console.WriteLine($"Singleton:{userSingletonService1.GetHashCode() == userSingletonService2.GetHashCode()}");

            using (var scope = serviceProvider.CreateScope())
            {
                var p = scope.ServiceProvider;
                var userScopeService1 = p.GetService<IUserScopeService>();
                var userScopeService2 = p.GetService<IUserScopeService>();
                Console.WriteLine($"Scope:{userScopeService1.GetHashCode() == userScopeService2.GetHashCode()}");
            }
            

            Console.ReadKey();
        }
    }
}
