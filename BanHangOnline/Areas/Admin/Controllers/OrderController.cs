using BanHangOnline.Models;
using BanHangOnline.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BanHangOnline.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class OrderController : Controller
	{
		private readonly DataContext _dataContext;
		public OrderController(DataContext context)
		{
			_dataContext = context;

		}
		public async Task<IActionResult> Index()
		{
			return View(await _dataContext.Orders.OrderByDescending(c => c.Id).ToListAsync());
		}

        public async Task<IActionResult> ViewOrder(string ordercode)
        {
			var DetailsOrder = await _dataContext.OrderDetails.Include(o => o.Product).Where(o => o.OrderCode == ordercode).ToListAsync();
            return View(DetailsOrder);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            OrderModel orders = await _dataContext.Orders.FindAsync(Id);
            _dataContext.Orders.Remove(orders);
            await _dataContext.SaveChangesAsync();
            TempData["sucess"] = "Xóa đơn hàng thành công";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Search(string searchString)
        {

            var products = await _dataContext.Orders
                .Where(p => p.Username.Contains(searchString))
                .ToListAsync();

            return View("Index", products);
        }
    }
}
