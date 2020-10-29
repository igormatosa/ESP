
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Aucxis.Eprw.Reporting.Dataservice;
using Microsoft.EntityFrameworkCore;
using Aucxis.Eprw.Reporting.Dataservice.Entities;
using Aucxis.Eprw.Reporting.Dataservice.Context;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Aucxis.Eprw.Reporting.Webapi
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
            services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
                .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));
            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Reporting API",
                    Description = "ASP.NET Core Web API",
                });
            });

            ConfigureDataRepositories(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Reporting API V1");
                c.DocExpansion(DocExpansion.None);
                c.DocumentTitle = "Reporting API V1";
                c.ShowExtensions();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureDataRepositories(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("AppDB");


            // Register repositories
            EprwServiceConfiguration.ConfigureRepositories(services, connectionString);

            // Register service
            EprwServiceConfiguration.ConfigureServices(services);
        }       
    }
}
