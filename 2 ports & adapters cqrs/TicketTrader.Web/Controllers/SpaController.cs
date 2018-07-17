using Microsoft.AspNetCore.Mvc;

namespace TicketTrader.Web.Controllers
{
    public class SpaController : Controller
    {
        public IActionResult Index(int eventId)
        {
            return Redirect($"/Spa/Store/SeatingPlan/{eventId}");
        }
    }
}