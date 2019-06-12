// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Idp.Dbs;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Idp.Repositorys;
using Idp.BLL;
using Idp.Models;

namespace Idp
{
    public class Startup
    {
        public IHostingEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //add ef core sql server
            string connecttext = Configuration.GetConnectionString("UserContext");
            services.AddDbContext<UserContext>(options => options.UseSqlServer(connecttext));

            //add user repository
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

            services.Configure<IISOptions>(options =>
            {
                options.AutomaticAuthentication = false;
                options.AuthenticationDisplayName = "Windows";
            });

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
              .AddProfileService<ProfileService>()
              .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();
            //.AddTestUsers(TestUsers.Users);

            // in-memory, code config
            builder.AddInMemoryIdentityResources(Config.GetIdentityResources());
            builder.AddInMemoryApiResources(Config.GetApis());
            builder.AddInMemoryClients(Config.GetClients());

            // in-memory, json config json配置方式
            //builder.AddInMemoryIdentityResources(Configuration.GetSection("IdentityResources"));
            //builder.AddInMemoryApiResources(Configuration.GetSection("ApiResources"));
            //builder.AddInMemoryClients(Configuration.GetSection("clients"));

            if (Environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("need to configure key material");
            }

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    // register your IdentityServer with Google at https://console.developers.google.com
                    // enable the Google+ API
                    // set the redirect URI to http://localhost:5000/signin-google
                    options.ClientId = "copy client ID from Google here";
                    options.ClientSecret = "copy client secret from Google here";
                });
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            InitDataBase(app);//初始化数据

            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="app"></param>
        public void InitDataBase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<UserContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<UserContext>();
                context.Database.Migrate();
                if (!context.Users.Any())
                {
                    User user = new User()
                    {
                        //UserId = "1",
                        UserName = "czhsoft",
                        Password = "czhsoft",
                        IsActive = true,
                        Claims = new List<Claims>
                        {
                            new Claims(){ Type="role", Value="admin" },
                            new Claims(){ Type="name", Value="czhsoft" },
                            new Claims(){ Type="given_name", Value="zh" },
                            new Claims(){ Type="family_name", Value="chen" },
                            new Claims(){ Type="email", Value="chenandczh@163.com" },
                            new Claims(){ Type="website", Value="https://github.com/CZHSoft" },
                        }
                    };
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
        }

    }
}