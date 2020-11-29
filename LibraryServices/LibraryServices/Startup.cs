using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryServices.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace LibraryServices
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
            var appSettings = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettings);
            AppSettings.DataBasePath = appSettings.GetSection("DataBasePath")?.Value;
            AppSettings.LogFile = appSettings.GetSection("LogFile")?.Value;
            AppSettings.AllowedOrigins = appSettings.GetSection("AllowedOrigins")?.Value;
            AppSettings.SampleFile = appSettings.GetSection("SampleFile")?.Value;
            string[] origins = null;
            if(!string.IsNullOrEmpty(AppSettings.AllowedOrigins))
            {
                if(AppSettings.AllowedOrigins.Contains(";"))
                {
                    origins = AppSettings.AllowedOrigins.Split(";", StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    origins = new string [] { AppSettings.AllowedOrigins };
                }
            }
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",
                   builder => builder.WithOrigins(origins)
                   .AllowAnyMethod().AllowAnyHeader());

                });
            
            services.AddControllers();
            services.AddSwaggerGen(c=> c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Get All Books",
                Description = "This Api fetches all the books with author and availability details",
                
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors("CorsPolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LetUsRead-GetAllBooksAPI");
               // c.RoutePrefix = string.Empty;
            });
            loggerFactory.AddFile(AppSettings.LogFile);
            
            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
