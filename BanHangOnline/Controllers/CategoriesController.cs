using BanHangOnline.Models;
using BanHangOnline.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BanHangOnline.Controllers
{
	public class CategoriesController : Controller
	{
		private readonly DataContext _dataContext;
		public CategoriesController(DataContext context)
		{
			_dataContext = context;
		}
		public async Task<IActionResult> Index(string Slug = "")
		{
			CategoriesModel categories = _dataContext.Categories.Where(c =>  c.Slug == Slug).FirstOrDefault();
			if (categories == null)
			{
				return RedirectToAction("Index");
			}
			var productByCategories = _dataContext.Products.Where(c => c.CategoriesId == categories.Id);

			return View(await productByCategories.OrderByDescending(p => p.Id).ToListAsync());
		}
	}
}
