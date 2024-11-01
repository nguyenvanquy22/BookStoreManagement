using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLCuaHangBanSach.Models;
using System.ComponentModel;

namespace QLCuaHangBanSach.Controllers
{
    public class NhanVienController : Controller
    {
        private DBCuaHangBanSachContext db;
        public NhanVienController(DBCuaHangBanSachContext _context)
        {
            this.db = _context;
        }
        public IActionResult Index()
        {
            List<Nhanvien> listNhanViens = db.Nhanviens.ToList();
            return View(listNhanViens);
        }

        //create
        public IActionResult ThemNhanVien()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult ThemNhanVien(Nhanvien nhanvien) 
        {
            List<Nhanvien> nhanviens = db.Nhanviens.ToList();
            if (nhanviens.Count > 0)
            {
                string tam = nhanviens.Last<Nhanvien>().MaNv.ToString();
                string[] luu = tam.Split('V');
                int tam1 = Convert.ToUInt16(luu[1]);
                tam1 += 1;
                luu[1] = tam1.ToString();
                tam = "";
                luu[0] += "V";
                for (int i = 0; i < 5 - luu[0].Length - luu[1].Length; i++)
                {
                    tam += "0";
                }
                nhanvien.MaNv = luu[0] + tam + luu[1];
            }
            else
            {
                nhanvien.MaNv = "NV001";
            }
            if (nhanvien.TenNv != null)
            {
                db.Nhanviens.Add(nhanvien);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }  
        }
        //sua
        public IActionResult SuaNhanVien(String manv)
        {

            if (manv == null || db.Nhanviens == null)
            {
                return NotFound();
            }
            var nhanvien = db.Nhanviens.Find(manv);
            if (nhanvien == null)
            {
                return NotFound();
            }
            return View(nhanvien);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(String manv, Nhanvien nhanvien)
        {
            if (manv != nhanvien.MaNv)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(nhanvien);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanVienExists(nhanvien.MaNv))
                        return NotFound();
                    else throw;

                }
                return RedirectToAction(nameof(Index));

            }

            return View(nhanvien);
        }
        private bool NhanVienExists(String manv)
        {
            return( db.Nhanviens?.Any(e => e.MaNv == manv)).GetValueOrDefault();
        }

        //xoa
        public IActionResult XoaNhanVien(String manv)
        {
            if (manv == null || db.Nhanviens == null)
            {
                return NotFound();
            }
            var nhanvien = db.Nhanviens.Include(l => l.Hdms)
            .Include(e => e.Hdbs)
            .FirstOrDefault(m => m.MaNv == manv);
            if (nhanvien == null)
            {
                return NotFound();
            }
            if (nhanvien.Hdbs.Count() > 0)
            {
                return Content("Không thể xóa!");
            }
            return View(nhanvien);
        }
        [HttpPost, ActionName("XoaNhanVien")]
        [ValidateAntiForgeryToken]
        public IActionResult XacNhanXoa(String manv)
        {
            if (db.Nhanviens == null)
            {
                return Problem("Không có nhân viên!");
            }
            var learner = db.Nhanviens.Find(manv);
            if (learner != null)
            {
                db.Nhanviens.Remove(learner);
            }
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
