using Veloci.Logic;
using Veloci.Logic.Services;
using Veloci.Web.Bot;

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