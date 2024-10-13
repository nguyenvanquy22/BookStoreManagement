using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QLCuaHangBanSach.Models;

namespace QLCuaHangBanSach.Controllers
{
    public class ProductsController : Controller
    {
        private DBCuaHangBanSachContext db;

        public ProductsController(DBCuaHangBanSachContext context)
        {
            
            this.db = context;
        }
        public IActionResult Index()
        {

            List<Sach> sachs = db.Saches.ToList();
            return View(sachs);
        }
        public IActionResult ProductDetail(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var sach = db.Saches.Find(id);
            if (sach == null)
            {
                return NotFound();
            }
            return View(sach);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Sach sach)
        {
            if (ModelState.IsValid)
            {
                db.Saches.Add(sach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var sach = db.Saches.Find(id);
            if (sach == null)
            {
                return NotFound();
            }
            return View(sach);
        }

        [HttpPost]
        public IActionResult edit(string id, Sach sach)
        {
            if (id != sach.MaSach)
            {
                return NotFound(id);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Saches.Update(sach);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!sachExists(sach.MaSach))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(sach);
        }
        private bool sachExists(string id)
        {
            return (db.Saches?.Any(e => e.MaSach == id)).GetValueOrDefault();
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound(id);
            }
            var sach = db.Saches.FirstOrDefault(m => m.MaSach == id);
            if (sach == null)
            {
                return NotFound();
            }
            return View(sach);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult Deleteconfirmed(string id)
        {
            if(db.Saches == null)
            {
                return Problem("Null");
            }
            var sach = db.Saches.Find(id);
            if(sach != null)
            {
                db.Saches.Remove(sach);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
