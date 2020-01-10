using System;
using System.Threading.Tasks;
using NovaCash.Sportsbook.Clients.Configurations;
using NovaCash.Sportsbook.Clients.Consts;
using NovaCash.Sportsbook.Clients.Criteria;
using NovaCash.Sportsbook.Clients.Models;
using NovaCash.Sportsbook.Clients.Repositories;
using NovaCash.Sportsbook.Clients.Requests;
using Refit;

namespace NovaCash.Sportsbook.Clients.Services
{
    public class SportsbookService
    {
        private readonly ISportsbookApi sportsbookApi;
        private readonly IBetDetailRepository repository;

        public SportsbookService(ISportsbookApi sportsbookApi = null, IBetDetailRepository repository = null)
        {
            this.sportsbookApi = sportsbookApi ?? RestService.For<ISportsbookApi>(AppSettings.Settings.APIURL);
            this.repository = repository ?? new BetDetailRepository();
        }

        public async Task<BetDetailResult> GetBetDetail()
        {
            BetDetailResult result;

            try
            {
                var lastVersionKey = repository.SelectBetDetailLastVersion(new SelectBetDetailLastVersionCriteria());
                var apiParams = new BetDetailRequest
                {
                    VendorId = AppSettings.Settings.APIVendorId,
                    VersionKey = lastVersionKey.ToString(),
                    Options = string.Empty,
                };

                result = await sportsbookApi.GetBetDetail(apiParams);
            }
            catch (Exception se)
            {
                result = new BetDetailResult
                {
                    error_code = BetDetailResultCode.ExcutionFailed,
                    message = se.Message
                };
            }

            return result;
        }
    }
}