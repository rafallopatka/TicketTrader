using Hangfire.Dashboard;

namespace TicketTrader.Management
{
    public class DashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            return true; //httpContext.User.Identity.IsAuthenticated;
        }
    }
}