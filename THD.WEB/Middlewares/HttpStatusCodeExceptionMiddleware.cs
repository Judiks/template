using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using THD.Core.Constants;
using THD.Core.Exceptions;

namespace THD.WEB.Middlewares
{
    public class HttpStatusCodeExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HttpStatusCodeExceptionMiddleware> _logger;

        public HttpStatusCodeExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = loggerFactory?.CreateLogger<HttpStatusCodeExceptionMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ApplicationCustomException exception)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                if (exception.StatusCode.HasValue)
                {
                    context.Response.StatusCode = exception.StatusCode.Value;
                }
                context.Response.ContentType = @"application/json";
                await context.Response.WriteAsync(exception.Message);
            }
            catch (UnauthorizeCustomException exception)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.Headers.Add(ExceptionConstants.InvalidRefresh, "true");
                context.Response.ContentType = @"application/json";
                await context.Response.WriteAsync(exception.Message);
            }
            catch (Exception exception)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
                    throw;
                }
                _logger.LogCritical($"Message={exception.Message}/StackTrace={exception.StackTrace}");
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = @"application/json";
                await context.Response.WriteAsync("InnerExeption : " + exception.InnerException);
            }
        }
    }
}
