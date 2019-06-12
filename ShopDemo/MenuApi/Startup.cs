
using MenuApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace MenuApi
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
            //add redis service
            services.AddTransient<IRedisService, RedisService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddDistributedRedisCache(options =>
            //{
            //    options.InstanceName = "";
            //    options.Configuration = "127.0.0.1:6379,password=123456,connectTimeout=2000";
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
