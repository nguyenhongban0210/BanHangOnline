using BanHangOnline.Models;
using BanHangOnline.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BanHangOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _dataContext;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _dataContext = context;
        }

        public IActionResult Index()
        {
            var products = _dataContext.Products.Include("Categories").Include("Brand").ToList();
            return View(products);
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

		public async Task<IActionResult> Search(string searchString)
		{

			var products = await _dataContext.Products
				.Include("Categories")
				.Include("Brand")
				.Where(p => p.Name.Contains(searchString) || p.Description.Contains(searchString))
				.ToListAsync();

			return View("Index", products);
		}
	}
}
