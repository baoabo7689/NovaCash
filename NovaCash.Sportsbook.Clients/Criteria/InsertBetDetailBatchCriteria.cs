using System.Linq;
using NovaCash.Sportsbook.Clients.Models;

namespace NovaCash.Sportsbook.Clients.Criteria
{
    public class InsertBetDetailBatchCriteria
    {
        public BetDetailResult BetDetailResult { get; set; }

        public bool IsValid() =>
            BetDetailResult.Data != null
            && BetDetailResult.Data.BetDetails != null
            && BetDetailResult.Data.BetDetails.Any();
    }
}