using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using OrderApi.Dbs;
using OrderApi.Repositorys;

namespace OrderApi
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
            //add ef core sql server
            string connecttext = Configuration.GetConnectionString("OrderContext");
            services.AddDbContext<OrderContext>(options => options.UseSqlServer(connecttext));

            //add order repository
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddMvcCore()
            .AddAuthorization()
            .AddJsonFormatters();

            //添加认证服务
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;

                    options.Audience = "orderapi"; //对应认证服务器api资源
                });

            //add CAP
            services.AddCap(x =>
            {
                x.UseEntityFramework<OrderContext>(); // EF

                x.UseSqlServer(connecttext); // SQL Server

                x.UseRabbitMQ(cfg =>
                {
                    cfg.HostName = "127.0.0.1";
                    cfg.Port = 5672;
                    cfg.UserName = "test1";
                    cfg.Password = "test1";
                }); // RabbitMQ

                x.UseDashboard(); // Dashboard

                // Below settings is just for demo
                x.FailedRetryCount = 2;
                x.FailedRetryInterval = 5;
            });

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();//使用认证服务

            app.UseMvc();

            //app.UseCap();//2.3以后不再需要
        }
    }
}
