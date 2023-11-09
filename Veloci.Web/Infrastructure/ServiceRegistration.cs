using Veloci.Logic.Services;

namespace Veloci.Web.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection RegisterCustomServices(this IServiceCollection services)
    {
        services.AddTransient<ResultsFetcher>();
        services.AddTransient<CompetitionService>();
        services.AddTransient<RaceResultsConverter>();
        services.AddTransient<MessageComposer>();
        
        return services;
    }
}