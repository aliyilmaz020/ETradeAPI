using ETradeAPI.Application.Repositories.CustomerRepositories;
using ETradeAPI.Application.Repositories.FileRepositories;
using ETradeAPI.Application.Repositories.InvoiceFileRepositories;
using ETradeAPI.Application.Repositories.OrderRepositories;
using ETradeAPI.Application.Repositories.ProductImageFileRepositories;
using ETradeAPI.Application.Repositories.ProductRepositories;
using ETradeAPI.Persistence.Contexts;
using ETradeAPI.Persistence.Repositories.CustomerRepositories;
using ETradeAPI.Persistence.Repositories.FileRepositories;
using ETradeAPI.Persistence.Repositories.InvoiceFileRepositories;
using ETradeAPI.Persistence.Repositories.OrderRepositories;
using ETradeAPI.Persistence.Repositories.ProductImageFileRepositories;
using ETradeAPI.Persistence.Repositories.ProductRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ETradeAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<ETradeApiContext>(opt => opt.UseNpgsql("User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=ETradeApiDB;"));

            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();
            services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
            services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();
            services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
            services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
        }
    }
}
