using Fanex.Data.Repository;
using Newtonsoft.Json;
using NovaCash.Sportsbook.Clients.Models;

namespace NovaCash.Sportsbook.Clients.Criteria
{
    public class InsertBetDetailCriteria : CriteriaBase
    {
        public BetDetail BetDetail { get; set; }

        public override string GetSettingKey() => "InsertBetDetail";

        public override bool IsValid() => BetDetail.trans_id > 0;

        public string GetSpParams() => JsonConvert.SerializeObject(BetDetail);
    }
}