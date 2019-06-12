
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MsgApi.Services;

namespace MsgApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //add order subscriber
            services.AddTransient<IOrderSubscriberService, OrderSubscriberService>();

            //add CAP
            services.AddCap(x =>
            {
                x.UseSqlServer("Data Source = E3\\SQLEXPRESS;Initial Catalog = order ;User Id = sa;Password = chenandczh@163.com ;"); // SQL Server

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

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            //app.UseCap();//2.3以后不再需要
        }
    }
}
