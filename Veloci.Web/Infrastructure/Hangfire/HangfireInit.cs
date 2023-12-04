using Hangfire;
using Hangfire.Storage;
using Veloci.Logic.Services;
using Veloci.Logic.Services.Tracks;

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

        RecurringJob.AddOrUpdate<CompetitionConductor>("Start new competition", x => x.StartNewAsync(), "2 17 * * *");
        RecurringJob.AddOrUpdate<CompetitionConductor>("Stop poll", x => x.StopPollAsync(), "58 16 * * *");
        RecurringJob.AddOrUpdate<CompetitionConductor>("Stop competition", x => x.StopAsync(), "0 17 * * *");

        RecurringJob.AddOrUpdate<CompetitionService>("Update results", x => x.UpdateResultsAsync(), "*/5 * * * *");
        RecurringJob.AddOrUpdate<CompetitionService>("Publish current leaderboard", x => x.PublishCurrentLeaderboardAsync(), "1 */2 * * *");
    }
}
