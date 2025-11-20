using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace ETradeAPI.Application.Middlewares
{
    public class GlobalExceptionMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var errors = ex.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                var json = JsonSerializer.Serialize(errors);
                await context.Response.WriteAsync(json);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var errorResponse = new
                {
                    Message = "An unexpected error occurred.",
                    Details = ex.Message
                };
                await context.Response.WriteAsync(
                     JsonSerializer.Serialize(new
                     {
                         Message = "Beklenmeyen bir hata oluştu.",
                         Detail = ex.Message
                     })
                );
            }
        }
    }
}
