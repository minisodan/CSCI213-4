using Microsoft.AspNetCore.Mvc;
using MusicStore.Data;
using MusicStore.Models;
using System.Diagnostics;

namespace MusicStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MusicStoreContext _context;
        public HomeController(ILogger<HomeController> logger, MusicStoreContext context)
        {
            _logger = logger;

            _context = context;

            var result = (from x in context.Music
                          select x);
            foreach(var item in result)
            {
                Storagecs.ts.Add(item.genre);
            }
            foreach(var item in result)
            {
                Storagecs.ts.Add(item.performer);
            }

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}