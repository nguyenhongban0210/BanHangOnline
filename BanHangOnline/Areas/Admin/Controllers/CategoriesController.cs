using BanHangOnline.Models;
using BanHangOnline.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BanHangOnline.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize]
	public class CategoriesController : Controller
	{
		private readonly DataContext _dataContext;

		public CategoriesController(DataContext context)
		{
			_dataContext = context;
			
		}

		public async Task<IActionResult> Index()
		{
			return View(await _dataContext.Categories.OrderByDescending(c => c.Id).ToListAsync());
		}

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriesModel categories)
        {
            if (ModelState.IsValid)
            {
                categories.Slug = categories.Name.Replace(" ", "_");
                var slug = await _dataContext.Categories.FirstOrDefaultAsync(p => p.Slug == categories.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Danh mục đã tồn tại");
                    return View(categories);
                }

                _dataContext.Add(categories);
                await _dataContext.SaveChangesAsync();
                TempData["sucess"] = "Thêm danh mục thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Model đang lỗi";
                List<string> errors = new List<string>();
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                string errorMessage = string.Join("\n", errors);
                return BadRequest(errorMessage);
            }
            return View(categories);
        }

        
        public async Task<IActionResult>Edit(int Id)
        {
            CategoriesModel categories = await _dataContext.Categories.FindAsync(Id);
            return View(categories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoriesModel categories)
        {
            if (ModelState.IsValid)
            {
                categories.Slug = categories.Name.Replace(" ", "_");
                var slug = await _dataContext.Categories.FirstOrDefaultAsync(p => p.Slug == categories.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Danh mục đã tồn tại");
                    return View(categories);
                }

                _dataContext.Update(categories);
                await _dataContext.SaveChangesAsync();
                TempData["sucess"] = "Cập nhật danh mục thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Model đang lỗi";
                List<string> errors = new List<string>();
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                string errorMessage = string.Join("\n", errors);
                return BadRequest(errorMessage);
            }
            return View(categories);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            CategoriesModel categories = await _dataContext.Categories.FindAsync(Id);
            _dataContext.Categories.Remove(categories);
            await _dataContext.SaveChangesAsync();
            TempData["sucess"] = "Xóa danh mục thành công";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Search(string searchString)
		{

			var products = await _dataContext.Categories
				.Where(p => p.Name.Contains(searchString) || p.Description.Contains(searchString))
				.ToListAsync();

			return View("Index", products);
		}
	}
}
