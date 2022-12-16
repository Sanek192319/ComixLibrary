using Core.Data;
using Data;
using Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace UI.ServiceExtesions;

public static class DataServices
{
    public static IServiceCollection InstallDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ComixLibContext>(x => x.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<IComixRepository, ComixRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
