using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SerilogTest.Models;
using System.Diagnostics;

namespace SerilogTest.Controllers
{
    public class HomeController : Controller
    {
        //program.cs i oku
        //anlatım https://www.youtube.com/watch?v=ewtPHH3cGd8

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogError("test hata mesajı 2");
            _logger.LogError("{@customer} hata mesajı", new Customer { Name = "Reşit" });//formatlı hata mesajı. seq de kolay arama var

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

    class Customer
    {
        public string Name { get; set; }
    }
}
