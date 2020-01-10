namespace NovaCash.Sportsbook.Clients.Criteria
{
    public class UpdateBetDetailLastVersionCriteria
    {
        public int Version { get; set; }

        public string GetSettingKey() => "UpdateBetDetailLastVersion";

        public bool IsValid() => Version > 0;
    }
}