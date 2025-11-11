using ETradeAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ETradeAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<ETradeApiContext>(opt => opt.UseNpgsql("User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=ETradeApiDB;"));
        }
    }
}
