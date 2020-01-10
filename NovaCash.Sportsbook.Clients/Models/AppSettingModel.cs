namespace NovaCash.Sportsbook.Clients.Models
{
    public class AppSettingModel
    {
        public string APIURL { get; set; }

        public string APIVendorId { get; set; }

        public int Currency { get; set; }

        public string HangfireConnection { get; set; }

        public string BetDetailConnection { get; set; }

        public string ExcelFolder { get; set; }

        public int GMT { get; set; }
    }
}