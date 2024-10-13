using Microsoft.AspNetCore.Mvc;
using QLCuaHangBanSach.Models;

namespace QLCuaHangBanSach.Controllers
{
	public class LoginController : Controller
	{
		private DBCuaHangBanSachContext db;
		public LoginController(DBCuaHangBanSachContext _context) 
		{
			db = _context;
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult CheckAccount(string username, string password)
		{
			if (username.Trim().Equals("") || password.Trim().Equals(""))
			{
				return NotFound();
			}	

			Nhanvien staff = db.Nhanviens.FirstOrDefault(n => n.TenDangNhap.Trim() == username.Trim());

				if(staff != null)
				{
					if(password.Trim().Equals(staff.MatKhau))
					{
						return Json(new { success = true, message = "Đăng nhập thành công" });
					}
					else
					{
						return Json(new { success = false, message = "Mật khẩu không đúng" });
					}
				}

			return Json(new { success = false, message = "Tài khoản không tồn tại" });
		}
	}
}

