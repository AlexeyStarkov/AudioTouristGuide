using AudioTouristGuide.WebAPI.Database;
using AudioTouristGuide.WebAPI.Database.Interfaces;
using AudioTouristGuide.WebAPI.Database.Repositories;
using AudioTouristGuide.WebAPI.SwaggerTools;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace AudioTouristGuide.WebAPI
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
            try
            {
                services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Audio Tourist Guide API", Version = "v1" });
                    options.OperationFilter<FileUploadOperationFilter>();
                });

                services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Development")));

                services.AddTransient<IToursRepository, ToursRepository>();
                services.AddTransient<IPlacesRepository, PlacesRepository>();
                services.AddTransient<IAudioAssetsRepository, AudioAssetsRepository>();
                services.AddTransient<IImageAssetsRepository, ImageAssetsRepository>();
            }
            catch (System.Exception ex)
            {

            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            try
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    app.UseHsts();
                }

                app.UseStaticFiles();
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Audio Tourist Guide API");
                });

                app.UseHttpsRedirection();
                app.UseMvc();

                using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                {
                    var dbContext = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();
                    dbContext.Database.Migrate();
                }
            }
            catch (System.Exception ex)
            {

            }
        }
    }
}
