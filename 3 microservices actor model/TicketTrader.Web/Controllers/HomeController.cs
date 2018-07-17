using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TicketTrader.Api.Client;
using TicketTrader.Web.ViewModels;

namespace TicketTrader.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _cache;

        public HomeController(HttpClientFactory factory, IMemoryCache memoryCache)
        {
            _httpClientFactory = factory;
            _cache = memoryCache;
        }

        public async Task<IActionResult> Index()
        {
            var eventsListCache = "eventsListCache";

            // Look for cache key.
            if (_cache.TryGetValue(eventsListCache, out EventListViewModel cachedViewModel))
            {
                return View(cachedViewModel);
            }

            using (var client = _httpClientFactory.CreateAuthorizedClient())
            {
                var baseUrl = Environment.GetEnvironmentVariable("TICKETTRADER_API_HOST");
                var apiClient = new EventsClient(baseUrl, client);
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


                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _cache.Set(eventsListCache, viewModel, cacheEntryOptions);

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
