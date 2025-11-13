using ETradeAPI.Application.Validators.Products;
using ETradeAPI.Application.ViewModels.Products;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ETradeAPI.Application
{
    public static class ValidationServices
    {
        public static void AddValidationServices(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateProductVM>, CreateProductValidator>();
        }
    }
}
