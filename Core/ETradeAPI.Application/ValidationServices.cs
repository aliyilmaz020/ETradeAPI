using ETradeAPI.Application.Features.Commands.Product.CreateProduct;
using ETradeAPI.Application.Validators.Products;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ETradeAPI.Application
{
    public static class ValidationServices
    {
        public static void AddValidationServices(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateProductCommandRequest>, CreateProductValidator>();
        }
    }
}
