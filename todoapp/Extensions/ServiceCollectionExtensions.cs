using System;
using Domain.Contracts;
using Domain.Services;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TodoApp.Extensions
{
    internal static class ServiceCollectionExtensions
	{
        internal static IServiceCollection RegisterAppContextServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<TodoDbContext>(options =>
                    {
                        options.UseSqlServer(configuration.GetConnectionString("TodoDatabse"),
                            // Retry on transient connection errors
                            opt =>
                            {
                                opt.EnableRetryOnFailure();
                            }
                        );
                    })
                .AddSwaggerGen();
            return services;
        }

        internal static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<ITodoRepository, TodoRepository>();
            return services;
        }

        internal static IServiceCollection RegisterService(this IServiceCollection services)
        {   
            services.AddTransient<ITodoService, TodoService>();
            return services;
        }
    }
}

