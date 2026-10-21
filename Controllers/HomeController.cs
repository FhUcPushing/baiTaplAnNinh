using baiTaplAnNinh.Data;
using baiTaplAnNinh.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
namespace baiTaplAnNinh.Controllers
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
            var products = CakeProductData.GetProducts();

            var model = new HomeViewModel
            {
                FeaturedProducts = products
                    .Where(p => p.Category == "Bánh Mousse"
                             || p.Category == "Bánh Kem"
                             || p.Category == "Bánh Sinh Nhật")
                    .Take(4)
                    .ToList(),

                FreshCakes = products
                    .Where(p => p.Category == "Bánh Tươi")
                    .Take(4)
                    .ToList()
            };

            return View(model);
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
