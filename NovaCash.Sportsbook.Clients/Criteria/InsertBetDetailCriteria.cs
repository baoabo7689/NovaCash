using Newtonsoft.Json;
using NovaCash.Sportsbook.Clients.Models;

namespace NovaCash.Sportsbook.Clients.Criteria
{
    public class InsertBetDetailCriteria
    {
        public BetDetail BetDetail { get; set; }

        public string GetSettingKey() => "InsertBetDetail";

        public bool IsValid() => BetDetail.trans_id > 0;

        public string GetSpParams() => JsonConvert.SerializeObject(BetDetail);
    }
}