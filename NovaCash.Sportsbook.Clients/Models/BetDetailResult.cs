namespace NovaCash.Sportsbook.Clients.Models
{
    public class BetDetailResult
    {
        public int error_code { get; set; }
        public string message { get; set; }
        public BetDetailData Data { get; set; }
    }
}