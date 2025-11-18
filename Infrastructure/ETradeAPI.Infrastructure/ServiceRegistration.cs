using ETradeAPI.Application.Services;
using ETradeAPI.Infrastructure.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;

namespace ETradeAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();
        }
    }
}
