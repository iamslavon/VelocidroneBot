using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace Veloci.Web.Infrastructure.Hangfire;

public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize([NotNull] DashboardContext context)
    {
        var userIdentity = context.GetHttpContext().User.Identity;
        return userIdentity is { IsAuthenticated: true };
    }
}