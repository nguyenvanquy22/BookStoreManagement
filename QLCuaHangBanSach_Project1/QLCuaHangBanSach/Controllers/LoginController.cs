using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using QLCuaHangBanSach.Models;
using System.Security.Claims;

namespace QLCuaHangBanSach.Controllers
{
	public class LoginController : Controller
	{
		private DBCuaHangBanSachContext db;
		public LoginController(DBCuaHangBanSachContext _context) 
		{
			db = _context;
		}
		public async Task<IActionResult> Index()
		{
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
		}

		public async Task<IActionResult> CheckAccount(string username, string password)
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
						var claims = new List<Claim>
						{
							new Claim(ClaimTypes.Name, staff.TenDangNhap)
						};
						
						var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
						var authProperties = new AuthenticationProperties { };

						await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

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

