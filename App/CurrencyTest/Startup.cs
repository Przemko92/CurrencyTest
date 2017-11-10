using System;
using CurrencyTest.Bootstraper;
using CurrencyTest.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace CurrencyTest
{
    public class Startup
    {
        private AppBoostraper _boostraper;
        public IConfiguration Configuration { get; }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<FixerConfiguration>(Configuration.GetSection("FixerConfiguration"));

            services.AddScoped(cfg => cfg.GetService<IOptions<FixerConfiguration>>().Value);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            return ConfigureIoC(services);
        }

        private IServiceProvider ConfigureIoC(IServiceCollection services)
        {
            _boostraper = new AppBoostraper();
            _boostraper.Init(services);
            return _boostraper.Resolve<IServiceProvider>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();
        }
    }
}
