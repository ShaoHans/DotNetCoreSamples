using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;
using Member.API.Data;
using Member.API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Member.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MemberDbContext>(options=> 
            {
                options.UseMySQL(Configuration.GetConnectionString("MemberCenterConnection"));
            });

            // 配置Consul服务注册地址
            services.Configure<ServiceDiscoveryOptions>(Configuration.GetSection("ServiceDiscovery"));
            // 配置Consul客户端
            services.AddSingleton<IConsulClient>(service =>
            new ConsulClient
            (
                config =>
                {
                    var serviceDisOptions = service.GetRequiredService<IOptions<ServiceDiscoveryOptions>>().Value;
                    if (!string.IsNullOrWhiteSpace(serviceDisOptions.Consul.HttpEndpoint))
                    {
                        config.Address = new Uri(serviceDisOptions.Consul.HttpEndpoint);
                    }
                }
            ));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
            app.UseConsul();
        }

    }
}
