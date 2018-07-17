using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketTrader.Api.Client;
using TicketTrader.Web.ViewModels;

namespace TicketTrader.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClientFactory _httpClientFactory;

        public HomeController(HttpClientFactory factory)
        {
            _httpClientFactory = factory;
        }

        public async Task<IActionResult> Index()
        {
            using (var client = _httpClientFactory.CreateAuthorizedClient())
            {
                var apiClient = new EventsClient(client);
                var response = await apiClient.GetAsync();

                var list = response
                    .Result
                    .Select(x => new EventListItemViewModel
                {
                    Id = x.Id,
                    Authors= x.Authors,
                    Cast = x.Cast,
                    Categories = x.Categories.ToArray(),
                    DateTime = x.DateTime,
                    Description = x.Description,
                    Duration = x.Duration,
                    Title = x.Title
                });

                var viewModel = new EventListViewModel(list);

                return View(viewModel);
            }
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return new SignOutResult(new List<string>{"Cookies", "oidc"});
        }
    }
}
