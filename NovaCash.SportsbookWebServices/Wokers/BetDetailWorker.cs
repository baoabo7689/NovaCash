using Hangfire;
using NovaCash.Sportsbook.Clients.Criteria;
using NovaCash.Sportsbook.Clients.Repositories;
using NovaCash.Sportsbook.Clients.Services;

namespace NovaCash.SportsbookWebServices.Wokers
{
    public class BetDetailWorker
    {
        public void Run()
        {
            using (var server = new BackgroundJobServer())
            {
                RecurringJob.RemoveIfExists("HangfireBetDetailWorker");
                RecurringJob.AddOrUpdate("HangfireBetDetailWorker", () => GetBetDetails(), Cron.Minutely);
            }
        }

        public void GetBetDetails()
        {
            var service = new SportsbookService();

            var criteria = new InsertBetDetailBatchCriteria
            {
                BetDetailResult = service.GetBetDetail().GetAwaiter().GetResult()
            };

            var repository = new BetDetailRepository();
            repository.InsertBetDetailBatch(criteria);
        }
    }
}