using Hangfire;
using Hangfire.Storage;
using Veloci.Logic.Services;

namespace Veloci.Web.Infrastructure.Hangfire;

public class HangfireInit
{
    public static void InitRecurrentJobs(IConfiguration configuration)
    {
        using (var connection = JobStorage.Current.GetConnection()) 
        {
            foreach (var recurringJob in connection.GetRecurringJobs()) 
            {
                RecurringJob.RemoveIfExists(recurringJob.Id);
            }
        }
        
        RecurringJob.AddOrUpdate<CompetitionService>("Update results", x => x.UpdateResultsAsync(), "*/5 * * * *");
    }
}