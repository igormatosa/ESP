using Aucxis.Eprw.Reporting.Dataservice.Entities;
using Aucxis.Eprw.Reporting.Dataservice.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Aucxis.Eprw.Reporting.Dataservice.Context
{
    public static class EprwServiceConfiguration
    {
        public static void ConfigureRepositories(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            // Register database scope
            services.AddScoped<IDatabaseScope, AppDatabaseScope>();

            services.AddScoped<IGenericRepository<TestEntity>, GenericRepository<TestEntity>>();

            
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();

            services.AddScoped<TestService>();
        }
    }
}
