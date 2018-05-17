﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Member.API.Data;
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
            InitUser(app);
        }

        private void InitUser(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MemberDbContext>();
                context.Database.Migrate();

                if(!context.Users.Any())
                {
                    context.Users.Add(new Data.Entities.User { Name = "shz" });
                    context.SaveChanges();
                }
            }
        }
    }
}