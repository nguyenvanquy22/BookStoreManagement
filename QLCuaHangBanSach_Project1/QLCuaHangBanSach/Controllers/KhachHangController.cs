using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLCuaHangBanSach.Models;

namespace QLCuaHangBanSach.Controllers
{
    public class KhachHangController : Controller
    {
        private DBCuaHangBanSachContext db;
        public KhachHangController(DBCuaHangBanSachContext _context)
        {
            this.db = _context;
        }
        public IActionResult Index()
        {
           List<Khachhang> listKhachhangs = db.Khachhangs.ToList();
            return View(listKhachhangs);
        }
        public IActionResult ThemKhachHang()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ThemKhachHang(Khachhang khachHang)
        {
            
            List<Khachhang> khachhangs = db.Khachhangs.ToList();
            if (khachhangs.Count > 0)
            {
                string tam = khachhangs.Last<Khachhang>().MaKh.ToString();
                string[] luu = tam.Split('H');
                int tam1 = Convert.ToUInt16(luu[1]);
                tam1 += 1;
                luu[1] = tam1.ToString();
                tam = "";
                luu[0] += "H";
                for (int i = 0; i < 5 - luu[0].Length - luu[1].Length; i++)
                {
                    tam += "0";
                }
                khachHang.MaKh = luu[0] + tam + luu[1];
            }
            else
            {
                khachHang.MaKh = "KH001";
            }
            if(khachHang.TenKh !="" && khachHang.Sdt!="" && khachHang.DiaChi!="") {
                db.Khachhangs.Add(khachHang);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
               return View();
            }
        }
        //Update
        public IActionResult SuaKhachHang(String id)
        {
            if(id == null || db.Khachhangs == null)
            {
                return NotFound();
            }
            var khachHang = db.Khachhangs.Find(id);
            if (khachHang == null)
            {
                return NotFound();
            }
            return View(khachHang);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaKhachHang(String id, Khachhang khachhang)
        {
            if(id != khachhang.MaKh)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(khachhang);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(khachhang);
        }

        //Xóa khách hàng
        public IActionResult XoaKhachHang(string id)
        {
            if(id==null || db.Khachhangs == null)
            {
                return NotFound();
            }
            var khachhang = db.Khachhangs.Find(id);
            if(khachhang == null)
            {
                return NotFound();
            }
            return View(khachhang);
        }
        [HttpPost, ActionName("XoaKhachHang")]
        [ValidateAntiForgeryToken]
        public IActionResult XoaKhachHangComfirmed(String id)
        {
            if (db.Khachhangs == null)
            {
                return NotFound("Không tồn tại khách hàng này");
            }
            var khachhang = db.Khachhangs.Find(id);
            if(khachhang != null)
            {
                db.Khachhangs.Remove(khachhang);
            }
            else
            {
                return NotFound(khachhang);
            }
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}