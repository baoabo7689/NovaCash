using Fanex.Data.Repository;

namespace NovaCash.Sportsbook.Clients.Criteria
{
    public class UpdateBetDetailLastVersionCriteria : CriteriaBase
    {
        public int Version { get; set; }

        public override string GetSettingKey() => "UpdateBetDetailLastVersion";

        public override bool IsValid() => Version > 0;
    }
}