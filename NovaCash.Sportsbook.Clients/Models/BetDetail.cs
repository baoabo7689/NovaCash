using System;

namespace NovaCash.Sportsbook.Clients.Models
{
    public class BetDetail
    {
        public long trans_id { get; set; }
        public string vendor_member_id { get; set; }
        public string operator_id { get; set; }
        public int? league_id { get; set; }
        public int match_id { get; set; }
        public int home_id { get; set; }
        public int away_id { get; set; }
        public DateTime match_datetime { get; set; }
        public int? sport_type { get; set; }
        public int bet_type { get; set; }
        public long parlay_ref_no { get; set; }
        public decimal odds { get; set; }
        public decimal stake { get; set; }
        public decimal validbetamount { get; set; }
        public DateTime transaction_time { get; set; }
        public string ticket_status { get; set; }
        public decimal winlost_amount { get; set; }
        public decimal after_amount { get; set; }
        public int currency { get; set; }
        public DateTime winlost_datetime { get; set; }
        public int odds_type { get; set; }
        public string isLucky { get; set; }
        public string bet_team { get; set; }
        public string exculding { get; set; }
        public decimal home_hdp { get; set; }
        public decimal away_hdp { get; set; }
        public object hdp { get; set; }
        public string betfrom { get; set; }
        public string islive { get; set; }
        public int? home_score { get; set; }
        public int? away_score { get; set; }
        public string customInfo1 { get; set; }
        public string customInfo2 { get; set; }
        public string customInfo3 { get; set; }
        public string customInfo4 { get; set; }
        public string customInfo5 { get; set; }
        public string ba_status { get; set; }
        public int version_key { get; set; }
    }
}