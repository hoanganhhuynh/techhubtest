using System;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using TodoApp.Middleware;

namespace TodoApp.Extensions
{
    internal static class WebApplicationBuilderExtension
	{
        internal static IApplicationBuilder RegisterDependencyServices(this IApplicationBuilder builder)
        {
            builder
                .UseMiddleware<ApiKeyMiddleware>()
                .UseMiddleware<ExceptionHandlerMiddleware>()
                .UseSwagger()
                .UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API");
                        c.RoutePrefix = string.Empty; //Configures swagger to load at application root
                    })
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });

            return builder;
        }
    }
}

