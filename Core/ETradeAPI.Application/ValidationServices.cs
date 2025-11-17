using ETradeAPI.Application.Features.Commands.Customer.CreateCustomer;
using ETradeAPI.Application.Features.Commands.Customer.RemoveCustomer;
using ETradeAPI.Application.Features.Commands.Customer.UpdateCustomer;
using ETradeAPI.Application.Features.Commands.Order.CreateOrder;
using ETradeAPI.Application.Features.Commands.Order.RemoveOrder;
using ETradeAPI.Application.Features.Commands.Order.UpdateOrder;
using ETradeAPI.Application.Features.Commands.Product.CreateProduct;
using ETradeAPI.Application.Features.Commands.Product.RemoveProduct;
using ETradeAPI.Application.Features.Commands.Product.UpdateProduct;
using ETradeAPI.Application.Validators.Customers;
using ETradeAPI.Application.Validators.Orders;
using ETradeAPI.Application.Validators.Products;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ETradeAPI.Application
{
    public static class ValidationServices
    {
        public static void AddValidationServices(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateProductCommandRequest>, CreateProductCommandValidator>();
            services.AddScoped<IValidator<UpdateProductCommandRequest>, UpdateProductCommandValidator>();
            services.AddScoped<IValidator<RemoveProductCommandRequest>, RemoveProductCommandValidator>();

            services.AddScoped<IValidator<UpdateCustomerCommandRequest>, UpdateCustomerCommandValidator>();
            services.AddScoped<IValidator<RemoveCustomerCommandRequest>, RemoveCustomerCommandValidator>();
            services.AddScoped<IValidator<CreateCustomerCommandRequest>, CreateCustomerCommandValidator>();

            services.AddScoped<IValidator<UpdateOrderCommandRequest>, UpdateOrderCommandValidator>();
            services.AddScoped<IValidator<RemoveOrderCommandRequest>, RemoveOrderCommandValidator>();
            services.AddScoped<IValidator<CreateOrderCommandRequest>, CreateOrderCommandValidator>();

        }
    }
}
