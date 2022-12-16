using Business.Services;
using Core.Business;
using Core.Options;

namespace UI.ServiceExtesions;

public static class BusinessServices 
{
    public static IServiceCollection InstallBusinessServices(this IServiceCollection services, IConfiguration configuration)
    {
        var fileSettings = new FileSettings();
        configuration.Bind(nameof(FileSettings), fileSettings);
        services.AddSingleton(fileSettings);
        services.AddScoped<IFileService, FileService>();
        return services;
    }
}
