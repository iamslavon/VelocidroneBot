using Veloci.Web.Bot;
using Veloci.Web.Services;

namespace Veloci.Web.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection RegisterCustomServices(this IServiceCollection services)
    {
        services.AddTransient<BotService>();
        services.AddTransient<ResultsFetcher>();
        
        return services;
    }
}