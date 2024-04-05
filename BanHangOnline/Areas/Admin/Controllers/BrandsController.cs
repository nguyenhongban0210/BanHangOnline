using BanHangOnline.Models;
using BanHangOnline.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BanHangOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BrandsController : Controller
	{
        private readonly DataContext _dataContext;

        public BrandsController(DataContext context)
        {
            _dataContext = context;

        }

        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Brands.OrderByDescending(b => b.Id).ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandModel brands)
        {
            if (ModelState.IsValid)
            {
                brands.Slug = brands.Name.Replace(" ", "_");
                var slug = await _dataContext.Brands.FirstOrDefaultAsync(b => b.Slug == brands.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Thương hiệu đã tồn tại");
                    return View(brands);
                }

                _dataContext.Add(brands);
                await _dataContext.SaveChangesAsync();
                TempData["sucess"] = "Thêm thương hiệu thành công";
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
            return View(brands);
        }

        public async Task<IActionResult> Edit(int Id)
        {
            BrandModel brands = await _dataContext.Brands.FindAsync(Id);
            return View(brands);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BrandModel brands)
        {
            if (ModelState.IsValid)
            {
                brands.Slug = brands.Name.Replace(" ", "_");
                var slug = await _dataContext.Brands.FirstOrDefaultAsync(p => p.Slug == brands.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Danh mục đã tồn tại");
                    return View(brands);
                }

                _dataContext.Update(brands);
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
            return View(brands);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            BrandModel brands = await _dataContext.Brands.FindAsync(Id);
            _dataContext.Brands.Remove(brands);
            await _dataContext.SaveChangesAsync();
            TempData["sucess"] = "Xóa thương hiệu thành công";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Search(string searchString)
        {

            var products = await _dataContext.Brands
                .Where(p => p.Name.Contains(searchString) || p.Description.Contains(searchString))
                .ToListAsync();

            return View("Index", products);
        }
    }
}
