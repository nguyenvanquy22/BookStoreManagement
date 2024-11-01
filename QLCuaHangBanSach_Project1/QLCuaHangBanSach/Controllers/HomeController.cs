using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLCuaHangBanSach.Models;
using System.Diagnostics;

namespace QLCuaHangBanSach.Controllers
{
	public class HomeController : Controller
	{
		private DBCuaHangBanSachContext db;

		public HomeController(DBCuaHangBanSachContext context)
		{
			this.db = context;
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		public IActionResult Index()
		{
			var minYears = db.Hdbs
				.Select(h => h.NgayBan.Year)
				.Distinct()
				.Min();
			ViewBag.MinYears  = minYears;

			// Total Sells
			var totalSells = db.ChiTietHdbs
								.Include(c => c.SoHdNavigation)
								.Where(c => c.SoHdNavigation.NgayBan.Year == DateTime.Now.Year)
								.GroupBy(c => c.SoHdNavigation.NgayBan.Year)
								.Select(g => new {
									TongGia = g.Sum(c => c.DonGia * c.SoLuong * (1 - c.GiamGia / 100)),
								})
								.FirstOrDefault();
			if (totalSells != null)
			{
				ViewBag.TotalSells = totalSells.TongGia;	
			}

			// Products Quantity
			var productsQuantity = new
			{
				SoDauSach = db.Saches.Count(),
				TongSoLuong = db.Saches.Sum(s => s.SoLuong)
			};
			if (productsQuantity != null)
			{
				ViewBag.ProductsQuantity = productsQuantity;
			}

			// Customers Quantity
			var customersQuantity = db.Hdbs
										.Include(h => h.MaKhNavigation)
										.Where(h => h.NgayBan.Year == DateTime.Now.Year)
                                        .Select(h => h.MaKhNavigation.MaKh)
										.Distinct()
										.Count();
            if (customersQuantity != 0)
			{
				ViewBag.CustomersQuantity = customersQuantity;
			}

			// Staffs Quantity
			var staffsQuantity = db.Nhanviens.Count();
			if (staffsQuantity != 0)
			{
				ViewBag.StaffsQuantity = staffsQuantity;
			}

			// Recent Invoices
			var recentInvoices = db.ChiTietHdbs
				.Include(c => c.SoHdNavigation).ThenInclude(h => h.MaKhNavigation)
				.Include(c => c.SoHdNavigation).ThenInclude(h => h.MaNvNavigation)
				.GroupBy(c => new { c.SoHdNavigation.SoHd, c.SoHdNavigation.MaNvNavigation.MaNv, c.SoHdNavigation.MaKhNavigation.MaKh })
				.Select(g => new {
					SoHD = g.Key.SoHd,
					NgayBan = g.Max(c => c.SoHdNavigation.NgayBan),
					TenNV = g.First().SoHdNavigation.MaNvNavigation.TenNv,
					TenKH = g.First().SoHdNavigation.MaKhNavigation.TenKh,
					TongHD = g.Sum(c => c.DonGia * c.SoLuong * (1 - c.GiamGia / 100)),
				})
				.OrderByDescending(c => c.NgayBan)
				.Take(10)
				.ToList();
			ViewBag.RecentInvoices = recentInvoices;

			// Top Staffs
			var topStaffs = db.ChiTietHdbs
				.Include(c => c.SoHdNavigation)
				.ThenInclude(h => h.MaNvNavigation)
				.GroupBy(nv => nv.SoHdNavigation.MaNvNavigation.MaNv)
				.Select(nv => new
				{
					MaNV = nv.Key,
					TenNV = nv.First().SoHdNavigation.MaNvNavigation.TenNv,
					TongSL = nv.Sum(c => c.SoLuong),
					TongThu = nv.Sum(c => c.DonGia * c.SoLuong * (1 - c.GiamGia / 100)),
				})
				.OrderByDescending(c => c.TongThu)
				.Take(10)
				.ToList();
			ViewBag.TopStaffs = topStaffs;

			// Top Products
			var topProducts = db.ChiTietHdbs
				.Include(c => c.MaSachNavigation)
				.GroupBy(c => new { c.MaSachNavigation.MaSach, c.MaSachNavigation.TenSach, c.MaSachNavigation.Anh })
				.Select(g => new
				{
					MaSach = g.Key.MaSach,
					TenSach = g.Key.TenSach,
					DonGia = g.First().DonGia,
					Anh = g.Key.Anh,
					TongSL = g.Sum(c => c.SoLuong),
					TongThu = g.Sum(c => c.DonGia * c.SoLuong * (1 - c.GiamGia / 100)),
				})
				.OrderByDescending(c => c.TongSL)
				.Take(10)
				.ToList();
			ViewBag.TopProducts = topProducts;

			return View();
		}

		public IActionResult GetDataForChart(int month,int year, bool isYearActive)
		{
			int[] data;
			if (isYearActive)
			{
				var dataHdb = db.ChiTietHdbs
								.Include(c => c.SoHdNavigation)
								.Where(c => c.SoHdNavigation.NgayBan.Year == year)
								.GroupBy(c => c.SoHdNavigation.NgayBan.Month)
								.OrderBy(g => g.Key)
								.Select(g => new {
									monthHdb = g.Key,
									TongGia = g.Sum(c => c.DonGia * c.SoLuong*(1-c.GiamGia/100)),
								})
								.ToList();
				data = new int[12];
				foreach (var d in dataHdb)
				{
					data[d.monthHdb-1] = d.TongGia;
				}
			}
			else
			{
				var dataHdb = db.ChiTietHdbs
								.Include(c => c.SoHdNavigation)
								.Where(c => c.SoHdNavigation.NgayBan.Year == year && 
											c.SoHdNavigation.NgayBan.Month == month)
								.GroupBy(c => c.SoHdNavigation.NgayBan)
								.OrderBy(g => g.Key)
								.Select(g => new {
									dayHdb = g.Key.Day,
									monthHdb = g.Key.Month,
									yearHdb = g.Key.Year,
									TongGia = g.Sum(c => c.DonGia * c.SoLuong* (1 - c.GiamGia/100)),
								})
								.ToList();
				data = new int[31];
				foreach (var d in dataHdb)
				{
					data[d.dayHdb-1] = d.TongGia;
				}
			}

			return Json(data);
		}
	}
}