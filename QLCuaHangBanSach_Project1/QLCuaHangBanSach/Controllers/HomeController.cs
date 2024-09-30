using Microsoft.AspNetCore.Mvc;

namespace QLCuaHangBanSach.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
