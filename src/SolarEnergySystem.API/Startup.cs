using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SolarEnergySystem.Core.Entities;
using SolarEnergySystem.Core.Interfaces;
using SolarEnergySystem.Core.Services;
using SolarEnergySystem.Infrastructure;
using SolarEnergySystem.Infrastructure.Repositories;

namespace SolarEnergySystem.API
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
            services.AddControllers();
            services.AddDbContext<SolarEnergySystemDatabaseContext>(options => options.UseInMemoryDatabase("SolarEnergy"));
            services.AddScoped<IPanelService, PanelService>();
            services.AddScoped<IRepository<Panel, string>, EntityFrameworkRepository<Panel, string>>();
            services.AddScoped<IElectricityReadingRepository, ElectricityReadingRepository>();
            services.AddScoped<IPanelRepository, PanelRepository>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<SolarEnergySystemDatabaseContext>();
                context.Database.EnsureCreated();
                context.SeedData();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
