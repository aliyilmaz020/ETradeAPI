using ETradeAPI.Application.Mapping;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ETradeAPI.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(cfg => cfg.AddProfile<GeneralMapping>());
        }
    }
}
