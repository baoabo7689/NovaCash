using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NovaCash.Sportsbook.Clients.Criteria;
using NovaCash.Sportsbook.Clients.Repositories;
using NovaCash.Sportsbook.Clients.Services;

namespace NovaCash.SportsbookServices.Workers
{
    public class BetDetailWorker : BackgroundService
    {
        private readonly ILogger<BetDetailWorker> logger;

        public BetDetailWorker(ILogger<BetDetailWorker> logger)
        {
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var service = new SportsbookService();

                    var criteria = new InsertBetDetailBatchCriteria
                    {
                        BetDetailResult = await service.GetBetDetail()
                    };

                    var repository = new BetDetailRepository();
                    repository.InsertBetDetailBatch(criteria);
                    await Task.Delay(5000, stoppingToken);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                }
            }
        }
    }
}