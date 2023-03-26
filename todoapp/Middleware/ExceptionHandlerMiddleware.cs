using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TodoApp.Middleware
{
    public sealed class ExceptionHandlerMiddleware
	{
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError("{@Exception}", ex);
                var innerException = ex.InnerException;

                while (innerException != null)
                {
                    _logger.LogError("{@InnerException}", innerException);
                    innerException = innerException.InnerException;
                }

                var errorDetail = ResolveError(ex);
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = errorDetail.ErrorCode;
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(errorDetail)).ConfigureAwait(false);
            }
        }

        private ErrorDetail ResolveError(Exception exception)
        {
            switch (exception.GetType().Name)
            {
                case "ApplicationException":
                    return new ErrorDetail((int)HttpStatusCode.Forbidden, exception.Message);
                case "KeyNotFoundException":
                    return new ErrorDetail((int)HttpStatusCode.NotFound, exception.Message);
                default:
                    return new ErrorDetail((int)HttpStatusCode.InternalServerError, "An unhandled exception has occurred, please check the log for details.");
            }
        }
        
    }
}

