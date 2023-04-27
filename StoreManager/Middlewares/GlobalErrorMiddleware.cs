using StoreManager.Exceptions;
using System.Text.Json;

namespace StoreManager.Middlewares
{
    public class GlobalErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DbNotFoundException exception)
            {
                await HandlerException(exception, context, 404);
            }
            catch (Exception exception)
            {
                await HandlerException(exception, context, 500);
            }
        }

        private static Task HandlerException(Exception ex, HttpContext context, int statusCode)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            var result = JsonSerializer.Serialize(new
            {
                errors = ex.Message,
                status = statusCode,
                title = "One or more errors occurred"
            });
            return context.Response.WriteAsync(result);
        }
    }
}
