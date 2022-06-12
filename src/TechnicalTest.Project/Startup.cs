using System.IO;
using FileContextCore;
using FileContextCore.FileManager;
using FileContextCore.Serializer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TechnicalTest.Project.Domain;
using TechnicalTest.Project.Infrastructure;
using TechnicalTest.Project.Infrastructure.Repositories;

namespace TechnicalTest.Project
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TestDbContext>(options =>
                options.UseFileContextDatabase<JSONSerializer, DefaultFileManager>(
                        location: $"{Directory.GetCurrentDirectory()}/Infrastructure/Database")
                    .EnableSensitiveDataLogging());
            services.AddScoped<IRepository<HealthFacility>, HealthFacilityRepository>();
            services.AddScoped<IRepository<HealthFacilityService>, HealthFacilityServiceRepository>();
            services.AddScoped<IRepository<Practitioner>, PractitionerRepository>();
            services.AddScoped<IRepository<PractitionerService>, PractitionerServiceRepository>();
            services.AddScoped<IRepository<Service>, ServiceRepository>();
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Include
            };

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}