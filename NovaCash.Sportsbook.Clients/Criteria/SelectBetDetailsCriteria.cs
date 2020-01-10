using Fanex.Data.Repository;

namespace NovaCash.Sportsbook.Clients.Criteria
{
    public class SelectBetDetailsCriteria : CriteriaBase
    {
        public override string GetSettingKey() => "SelectBetDetails";

        public override bool IsValid() => true;
    }
}