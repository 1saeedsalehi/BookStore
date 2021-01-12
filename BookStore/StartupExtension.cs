using BookStore.Data;
using BookStore.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore
{
    internal static class StartupExtension
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.DescribeAllEnumsAsStrings();
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Book Store 😎",
                    Version = "v1",
                    Description = "BookStore Api",
                });
            });
            return services;
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("BookStoreSettings:ConnectionString");
            services.AddDbContextPool<BookStoreDbContext>(opt => opt.UseSqlServer(connectionString));

            return services;
        }

        public static void UseErrorHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
