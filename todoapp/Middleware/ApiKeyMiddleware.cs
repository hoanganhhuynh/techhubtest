using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace TodoApp.Middleware
{
	public class ApiKeyMiddleware
	{
        private const string ApiKeyHeaderKey = "x-api-key";
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiKeyMiddleware> _logger;

        public ApiKeyMiddleware(RequestDelegate next,
            ILogger<ApiKeyMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (!httpContext.Request.Path.StartsWithSegments("/retool"))
            {
                await _next.Invoke(httpContext);
                return;
            }

            var bResult = httpContext.Request.Headers.TryGetValue(ApiKeyHeaderKey, out StringValues apiKeyCandidates);
            var secretKey = "2wDf5xsR7F";

            if (bResult && apiKeyCandidates.Any(x => x == secretKey))
            {
                await _next.Invoke(httpContext);
                return;
            }

            httpContext.Response.ContentType = "application/json";
            ErrorDetail errorDetail = new ErrorDetail((int)HttpStatusCode.Unauthorized, $"{ApiKeyHeaderKey} not found");

            httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(errorDetail));
        }
    }
}

