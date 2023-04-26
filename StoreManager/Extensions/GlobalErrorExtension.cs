using StoreManager.Middlewares;

namespace StoreManager.Extensions
{
    public static class GlobalErrorExtension
    {
        public static IApplicationBuilder UseGlobalError(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalErrorMiddleware>();
            return app;
        }
    }
}
