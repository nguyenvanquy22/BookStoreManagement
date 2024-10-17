using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLCuaHangBanSach.Models;

namespace QLCuaHangBanSach.Controllers
{
	public class ImportInvoiceController : Controller
	{
		private DBCuaHangBanSachContext db;

        private int invoiceNumber;

        private int supplierID;

        public ImportInvoiceController(DBCuaHangBanSachContext context)
		{
			db = context;
		}
		
		public IActionResult Index()
		{
			var invoices = db.Hdms.Include(staff => staff.MaNvNavigation)
									.Include(supplier => supplier.MaNccNavigation).ToList();
			return View(invoices);
		}

		[HttpGet]
		public IActionResult Create()
		{
			var books = db.Saches.ToList();
			ViewBag.Books = books;
			return View();
		}

		[HttpPost]
		public IActionResult GetData([FromBody]
									ImportInvoiceViewModel dataInput)
		{
			var suppliers = db.Nhacungcaps.ToList();
			Hdm importInvoice = dataInput.ImportInvoice;
			List<ChiTietHdm> importInvoiceDetails = dataInput.ImportInvoiceDetails;

			foreach (var c in suppliers)
			{
				if (c.Sdt != null)
				{
					if (c.TenNcc.Equals(importInvoice.MaNccNavigation.MaNcc) && c.Sdt.Equals(importInvoice.MaNccNavigation.Sdt))
					{
                        importInvoice.MaNcc = c.MaNcc;
                        importInvoice.MaNccNavigation.MaNcc = c.MaNcc;
					}
				}
			}

			Nhacungcap existingSupplier = db.Nhacungcaps.FirstOrDefault(c => c.MaNcc == importInvoice.MaNcc);
			if (existingSupplier == null)
			{
                Nhacungcap newSupplier = new Nhacungcap
                {
					TenNcc = importInvoice.MaNccNavigation.TenNcc,
					Sdt = importInvoice.MaNccNavigation.Sdt is null ? "" : importInvoice.MaNccNavigation.Sdt,
					DiaChi = importInvoice.MaNccNavigation.DiaChi is null ? "" : importInvoice.MaNccNavigation.DiaChi,
					MaNcc = GenerateSupplierID()
				};
				db.Nhacungcaps.Add(newSupplier);
				db.SaveChanges();

				importInvoice.MaNccNavigation = newSupplier;
			}
			else
			{
				if (importInvoice.MaNccNavigation.DiaChi is not null)
				{
					existingSupplier.DiaChi = importInvoice.MaNccNavigation.DiaChi;
					db.Nhacungcaps.Update(existingSupplier);
				}
				importInvoice.MaNccNavigation = existingSupplier;
			}

			importInvoice.SoHd = GenerateInvoiceID();
			importInvoice.NgayNhap = DateTime.Now;
			importInvoice.MaNv = "NV001";
			db.Hdms.Add(importInvoice);

			foreach (var i in importInvoiceDetails)
			{
				i.SoHd = importInvoice.SoHd;
				var book = db.Saches.FirstOrDefault(b => b.MaSach == i.MaSach);
				book.SoLuong += i.SoLuong;
				db.Update(book);
				db.ChiTietHdms.Add(i);
			}
			db.SaveChanges();

			return Json(new { success = true });
		}
		public string GenerateInvoiceID()
		{
			var lastInvoice = db.Hdms.OrderByDescending(i => i.SoHd).FirstOrDefault();

			if (lastInvoice != null)
			{
				// Lấy mã hóa đơn cuối cùng và tăng giá trị lên 1
				var lastCode = lastInvoice.SoHd;
				var lastNumber = int.Parse(lastCode.Substring(3));
				var nextNumber = lastNumber + 1;

				// Tạo mã hóa đơn mới
				var nextCode = "HDM" + nextNumber.ToString("000");

				return nextCode;
			}
			else
			{
				// Nếu không có mã hóa đơn nào trong cơ sở dữ liệu, bắt đầu với "HD001"
				return "HDM001";
			}
		}

		public string GenerateSupplierID()
		{
			supplierID = db.Nhacungcaps.Count() + 1;
			return "NCC" + supplierID.ToString("D3"); // D3 để đảm bảo có 3 chữ số
		}

		[HttpPost]
		public IActionResult Delete(string id)
		{
			if (id == null || db.Hdms == null)
				return NotFound();

			var importInvoice = db.Hdms.FirstOrDefault(e => e.SoHd == id);
			if (importInvoice == null)
				return NotFound();
			var importInvoiceDetails = db.ChiTietHdms.Where(i => i.SoHd == id).ToList();
			foreach (var i in importInvoiceDetails)
			{
				db.ChiTietHdms.Remove(i);
			}
			db.Hdms.Remove(importInvoice);
			db.SaveChanges();
			return Json(new { success = true });
		}
		public IActionResult InvoiceDetails(string id)
		{
			string id1 = id;
			var details = db.ChiTietHdms.Where(d => d.SoHd == id1).Include(b => b.MaSachNavigation).ToList();
			var invoice = db.Hdms.Include(staff => staff.MaNvNavigation)
									.Include(customer => customer.MaNccNavigation).FirstOrDefault(i => i.SoHd == id1);
			ViewBag.Id = id1;
			if (invoice.MaNccNavigation.TenNcc != null)
			{
				ViewBag.TenNcc = invoice.MaNccNavigation.TenNcc;
			}
			if (invoice.MaNccNavigation.DiaChi != null)
			{
				ViewBag.DiaChi = invoice.MaNccNavigation.DiaChi;
			}
			if (invoice.MaNccNavigation.Sdt != null)
			{
				ViewBag.Sdt = invoice.MaNccNavigation.Sdt;
			}
			double s = 0;
			foreach (var item in details)
			{
				s += (double)(item.SoLuong * item.DonGia);
			}
			ViewBag.TongTien = s;
			ViewBag.VAT = (double)invoice.VAT;
			ViewBag.ChietKhau = (double)invoice.ChietKhau;
			ViewBag.VATAmount = s * (double)(invoice.VAT) / 100;
			ViewBag.ChietKhauAmount = s * (double)(invoice.ChietKhau) / 100;
			ViewBag.ConLai = (double)invoice.TongTien;
			return View(details);
		}
	}
}
