using BanHangOnline.Models;
using BanHangOnline.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BanHangOnline.Controllers
{
	public class BrandsController : Controller
	{
		private readonly DataContext _dataContext;

		public BrandsController(DataContext context)
		{
			_dataContext = context;
		}
		public async Task<IActionResult> Index(string Slug = "")
		{
			BrandModel brand = _dataContext.Brands.Where(c => c.Slug == Slug).FirstOrDefault();
			if (brand == null)
			{
				return RedirectToAction("Index");
			}
			var productByBrands = _dataContext.Products.Where(c => c.BrandId == brand.Id);

			return View(await productByBrands.OrderByDescending(p => p.Id).ToListAsync());
		}
	}
}
