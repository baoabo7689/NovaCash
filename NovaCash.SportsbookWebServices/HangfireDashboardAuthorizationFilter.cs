using Hangfire.Dashboard;

namespace NovaCash.SportsbookWebServices
{
    public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context) => true;
    }
}