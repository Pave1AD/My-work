using PseuSM.Middlewares;

namespace PseuSM.Extensions
{
    public static class ErrorMiddlewareExtension
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
