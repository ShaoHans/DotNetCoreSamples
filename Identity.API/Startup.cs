﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DnsClient;
using Identity.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Identity.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddExtensionGrantValidator<SmsCodeAuthGrantValidator>()
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryClients(Config.GetClients());

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISmsCodeService, SmsCodeService>();
            // 配置Consul服务发现地址
            services.Configure<ServiceDiscoveryOptions>(Configuration.GetSection("ServiceDiscovery"));
            services.AddSingleton<IDnsQuery>(p =>
            {
                var serviceDisOptions = p.GetRequiredService<IOptions<ServiceDiscoveryOptions>>().Value;
                return new LookupClient(serviceDisOptions.Consul.DnsEndpoint.ToIPEndPoint());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
        }
    }
}
