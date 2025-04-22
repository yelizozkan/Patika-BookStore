using BookStore.Services;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

namespace BookStore.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;

        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            try
            {
                _loggerService.LogInfo($"[Request] HTTP {context.Request.Method} - {context.Request.Path}");

                await _next(context);
                watch.Stop();

                _loggerService.LogInfo($"[Response] HTTP {context.Request.Method} - {context.Request.Path} responded {context.Response.StatusCode} in {watch.Elapsed.TotalMilliseconds} ms");
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleExceptionAsync(context, ex, watch);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex, Stopwatch watch)
        {
            _loggerService.LogError($"[Exception] HTTP {context.Request.Method} - {context.Response.StatusCode} Error Message: {ex.Message} in {watch.Elapsed.TotalMilliseconds} ms");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonConvert.SerializeObject(new
            {
                error = ex.Message
            }, Formatting.None);

            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
