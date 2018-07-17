using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TicketTrader.Api.Core
{
    [Authorize]
    public abstract class ApiController : Controller
    {
        protected ILogger<ApiController> Logger { get; }
        public RequestHandler Respond { get; set; }

        protected ApiController(ILogger<ApiController> logger)
        {
            Logger = logger;
            Respond = new RequestHandler(this, logger);
        }
    }
}