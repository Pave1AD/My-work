using PseuSM.Models.Errors;
using System;

namespace PseuSM.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IWebHostEnvironment environment)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Error mappedError = ex switch
                {
                    ArgumentException => new ErrorWithInnerException(400, ex.Message, ex.InnerException?.Message!),
                    ArgumentNullException or InvalidOperationException or _ => new ErrorWithInnerException(500, ex.Message, ex.InnerException?.Message!)
                };

                context.Response.StatusCode = mappedError.StatusCode;

                if (environment.IsDevelopment())
                {
                    await context.Response.WriteAsJsonAsync((ErrorWithInnerException)mappedError);
                } 
                else
                {
                    await context.Response.WriteAsJsonAsync((ErrorWithMessage)mappedError);
                }
            }
        }
    }
}
