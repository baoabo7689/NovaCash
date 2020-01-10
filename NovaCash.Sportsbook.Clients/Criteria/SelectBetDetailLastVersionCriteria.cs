using Fanex.Data.Repository;

namespace NovaCash.Sportsbook.Clients.Criteria
{
    public class SelectBetDetailLastVersionCriteria : CriteriaBase
    {
        public override string GetSettingKey() => "SelectBetDetailLastVersion";

        public override bool IsValid() => true;
    }
}