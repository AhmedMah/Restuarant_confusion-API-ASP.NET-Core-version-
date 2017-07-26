using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using confusionresturant.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using confusionresturant.Repository;
using Newtonsoft.Json;
using AutoMapper;
using confusionresturant.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace confusionresturant
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
           _config = builder.Build();
        }
        IConfigurationRoot _config;
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);
            services.AddDbContext<CrContext>(ServiceLifetime.Scoped);
            services.AddScoped<ICrRepository, CrRepository>();
            services.AddTransient<CRInitializer>();
            services.AddIdentity<CrUser, IdentityRole>()
                    .AddEntityFrameworkStores<CrContext>();
            services.AddAutoMapper();
            // Add framework services.
            services.AddMvc()
                   .AddJsonOptions(opt =>
                   {
                       // ignore circular reference 
                       opt.SerializerSettings.ReferenceLoopHandling =
                       ReferenceLoopHandling.Ignore;
                   });
        }
       // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                             IHostingEnvironment env,
                             ILoggerFactory loggerFactory,
                             CRInitializer seeder)
        {
            loggerFactory.AddConsole(_config.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseIdentity();
            app.UseMvc();
            seeder.Seed().Wait();
        }
    }
}
