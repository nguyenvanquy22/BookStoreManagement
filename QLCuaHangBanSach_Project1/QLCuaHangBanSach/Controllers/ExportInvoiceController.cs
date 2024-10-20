using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLCuaHangBanSach.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace QLCuaHangBanSach.Controllers
{
	public class ExportInvoiceController : Controller
	{
		private DBCuaHangBanSachContext db;

        private int invoiceNumber;

		private int customerID;

        public ExportInvoiceController(DBCuaHangBanSachContext context)
		{
			db = context;
		}
		public IActionResult Index()
		{
			var invoices = db.Hdbs.Include(staff => staff.MaNvNavigation)
									.Include(customer => customer.MaKhNavigation).ToList();
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
                                    ExportInvoiceViewModel dataInput)
		{
            var customers = db.Khachhangs.ToList();
            Hdb exportInvoice = dataInput.ExportInvoice;
            List<ChiTietHdb> exportInvoiceDetails = dataInput.ExportInvoiceDetails;

            foreach (var c in customers)
            {
                if (c.Sdt != null)
                {
                    if (c.TenKh.Equals(exportInvoice.MaKhNavigation.TenKh) && c.Sdt.Equals(exportInvoice.MaKhNavigation.Sdt))
                    {
                        exportInvoice.MaKh = c.MaKh;
                        exportInvoice.MaKhNavigation.MaKh = c.MaKh;
                    }
                }
            }

            Khachhang existingCustomer = db.Khachhangs.FirstOrDefault(c => c.MaKh == exportInvoice.MaKh);
            if (existingCustomer == null)
            {
                Khachhang newCustomer = new Khachhang
                {
                    TenKh = exportInvoice.MaKhNavigation.TenKh,
                    Sdt = exportInvoice.MaKhNavigation.Sdt is null ? "" : exportInvoice.MaKhNavigation.Sdt,
                    DiaChi = exportInvoice.MaKhNavigation.DiaChi is null ? "" : exportInvoice.MaKhNavigation.DiaChi,
                    MaKh = GenerateCustomerID()
                };
                db.Khachhangs.Add(newCustomer);
                db.SaveChanges();

                exportInvoice.MaKhNavigation = newCustomer;
            }
            else
            {
                if (exportInvoice.MaKhNavigation.DiaChi is not null)
                {
                    existingCustomer.DiaChi = exportInvoice.MaKhNavigation.DiaChi;
                    db.Khachhangs.Update(existingCustomer);
                }
                exportInvoice.MaKhNavigation = existingCustomer;
            }

            exportInvoice.SoHd = GenerateInvoiceID();
            exportInvoice.NgayBan = DateTime.Now;
            exportInvoice.MaNv = "NV001";
            db.Hdbs.Add(exportInvoice);

            foreach (var i in exportInvoiceDetails)
            {
                i.SoHd = exportInvoice.SoHd;
                var book=db.Saches.FirstOrDefault(b => b.MaSach == i.MaSach);
                book.SoLuong -= i.SoLuong;
                db.Update(book);
                db.ChiTietHdbs.Add(i);

            }
            db.SaveChanges();

			return Json(new { success = true });
		}
        public string GenerateInvoiceID()
        {
			var lastInvoice = db.Hdbs.OrderByDescending(i => i.SoHd).FirstOrDefault();

			if (lastInvoice != null)
			{
				// Lấy mã hóa đơn cuối cùng và tăng giá trị lên 1
				var lastCode = lastInvoice.SoHd;
				var lastNumber = int.Parse(lastCode.Substring(3));
				var nextNumber = lastNumber + 1;

				// Tạo mã hóa đơn mới
				var nextCode = "HDB" + nextNumber.ToString("000");

				return nextCode;
			}
			else
			{
				// Nếu không có mã hóa đơn nào trong cơ sở dữ liệu, bắt đầu với "HD001"
				return "HDB001";
			}
		}

        public string GenerateCustomerID()
        {
            customerID = db.Khachhangs.Count() + 1;
            return "KH" + customerID.ToString("D3"); // D3 để đảm bảo có 3 chữ số
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            if (id == null || db.Hdbs == null)
                return NotFound();

            var ExportInvoice = db.Hdbs.FirstOrDefault(e => e.SoHd == id);
            if (ExportInvoice == null)
                return NotFound();
            var ExportInvoiceDetails = db.ChiTietHdbs.Where(i => i.SoHd == id).ToList();
            foreach( var i in ExportInvoiceDetails)
            {
                db.ChiTietHdbs.Remove(i);
            }
            db.Hdbs.Remove(ExportInvoice);
            db.SaveChanges();
            return Json(new { success = true });
		}
		public IActionResult InvoiceDetails(string id)
		{
            var details = db.ChiTietHdbs.Where(d => d.SoHd == id).Include(b => b.MaSachNavigation).ToList();
            var invoice = db.Hdbs.Include(staff => staff.MaNvNavigation)
                                    .Include(customer => customer.MaKhNavigation).FirstOrDefault(i => i.SoHd == id);
            ViewBag.Id = id;
            if (invoice.MaKhNavigation.TenKh != null)
            {
                ViewBag.TenKh = invoice.MaKhNavigation.TenKh;
            }
            if (invoice.MaKhNavigation.DiaChi != null)
            {
                ViewBag.DiaChi = invoice.MaKhNavigation.DiaChi;
            }
            if (invoice.MaKhNavigation.Sdt != null)
            {
                ViewBag.Sdt = invoice.MaKhNavigation.Sdt;
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
