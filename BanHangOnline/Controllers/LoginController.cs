using Microsoft.AspNetCore.Mvc;

namespace BanHangOnline.Controllers
{
	public class LoginController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
