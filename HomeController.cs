using berber.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BerberWebSitesi.Models;

namespace berber.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            // Hizmetleri tanımlıyoruz
            var services = new List<Service>
{
    new Service { Id = 1, Name = "Saç Yıkama", Duration = 15, Price = 50 },
    new Service { Id = 2, Name = "Fön", Duration = 15, Price = 100 },
    new Service { Id = 3, Name = "Cilt Bakımı", Duration = 30, Price = 150 },
    new Service { Id = 4, Name = "Cilt Maskesi", Duration = 15, Price = 100 },
    new Service { Id = 5, Name = "Saç Kesimi", Duration = 45, Price = 300 },
    new Service { Id = 6, Name = "Sakal Kesimi", Duration = 20, Price = 150 }
};


            // Görünüme (Razor) hizmetleri gönderiyoruz
            return View(services);
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
