using BanHangOnline.Models;
using BanHangOnline.Models.ViewModels;
using BanHangOnline.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BanHangOnline.Controllers
{
	[Authorize]
	public class CartController : Controller
	{
		private readonly DataContext _dataContext;
		public CartController(DataContext context)
		{
			_dataContext = context;
		}
		public IActionResult Index()
		{
			List<CartItemModel> cartItems = HttpContext.Session.GetJson
				<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			CartItemViewModel cartVM = new()
			{
				CartItems = cartItems,
				GrandTotal = cartItems.Sum(x => x.Quantity * x.Price),
			};
			return View(cartVM);
		}

		public IActionResult Checkout()
		{
			return View("~/Views/Checkout/Index.cshtml");
		}

		public async Task<IActionResult> Add(int Id)
		{
			ProductModel product = await _dataContext.Products.FindAsync(Id);
			List<CartItemModel> cart = HttpContext.Session.GetJson
				<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			CartItemModel cartItem = cart.Where(p => p.ProductId == Id).FirstOrDefault();

			if (cartItem == null)
			{
				cart.Add(new CartItemModel(product));
			}
			else
			{
				cartItem.Quantity += 1;
			}
			HttpContext.Session.SetJson("Cart", cart);
			TempData["sucess"] = "Đã thêm giỏ hàng thành công";
			return Redirect(Request.Headers["Referer"].ToString());
		}

		public async Task<IActionResult> Decrease(int Id)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
			CartItemModel cartItem = cart.Where(p => p.ProductId == Id).FirstOrDefault();
			if (cartItem.Quantity > 1)
			{
				--cartItem.Quantity;
			}
			else
			{
				cart.RemoveAll(p => p.ProductId == Id);
			}
			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}
			TempData["sucess"] = "Đã giảm số lượng sản phẩm";
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Increase(int Id)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
			CartItemModel cartItem = cart.Where(p => p.ProductId == Id).FirstOrDefault();
			if (cartItem.Quantity >= 1)
			{
				++cartItem.Quantity;
			}
			else
			{
				cart.RemoveAll(p => p.ProductId == Id);
			}
			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}
			TempData["sucess"] = "Đã tăng số lượng sản phẩm";
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Remove(int Id)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
			cart.RemoveAll(p => p.ProductId == Id);
			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}
			TempData["sucess"] = "Đã xóa sản phẩm ra khỏi giỏ hàng";
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Claer()
		{
			HttpContext.Session.Remove("Cart");
			TempData["sucess"] = "Đã xóa toàn bộ giỏ hàng";
			return RedirectToAction("Index");
		}

	}
}
