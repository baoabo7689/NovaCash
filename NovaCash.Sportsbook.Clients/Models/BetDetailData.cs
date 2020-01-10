using System.Collections.Generic;

namespace NovaCash.Sportsbook.Clients.Models
{
    public class BetDetailData
    {
        public int last_version_key { get; set; }
        public List<BetDetail> BetDetails { get; set; }
    }
}