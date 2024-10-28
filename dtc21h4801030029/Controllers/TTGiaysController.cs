using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace dtc21h4801030029.Models
{
    public class TTGiaysController : Controller
    {
        private BanGiay1Entities db = new BanGiay1Entities();

        // GET: TTGiays
        public ActionResult Index()
        {
            return View(db.TTGiays.ToList());
        }

        public ActionResult Search(string loaiGiay)
        {
            // Nếu loaiGiay là null hoặc rỗng, trả về danh sách tất cả các giày
            if (string.IsNullOrEmpty(loaiGiay))
            {
                return View("Index", db.TTGiays.ToList());
            }

            // Tìm các giày có LoaiGiay giống với chuỗi tìm kiếm
            var giays = db.TTGiays.Where(g => g.LoaiGiay.Contains(loaiGiay)).ToList();

            // Trả về kết quả tìm kiếm
            return View("Index", giays);
        }

        // GET: TTGiays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TTGiay tTGiay = db.TTGiays.Find(id);
            if (tTGiay == null)
            {
                return HttpNotFound();
            }
            return View(tTGiay);
        }

        // GET: TTGiays/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TTGiays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idGiay,LoaiGiay,ThongTin")] TTGiay tTGiay)
        {
            if (!int.TryParse(tTGiay.idGiay.ToString(), out int idGiay))
            {
                ModelState.AddModelError("idGiay", "ID Giày phải là một số nguyên hợp lệ.");
            }

            if (ModelState.IsValid)
            {
                db.TTGiays.Add(tTGiay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tTGiay);
        }



        // GET: TTGiays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TTGiay tTGiay = db.TTGiays.Find(id);
            if (tTGiay == null)
            {
                return HttpNotFound();
            }
            return View(tTGiay);
        }

        // POST: TTGiays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idGiay,LoaiGiay,ThongTin")] TTGiay tTGiay)
        {
            if (!int.TryParse(tTGiay.idGiay.ToString(), out int idGiay))
            {
                ModelState.AddModelError("idGiay", "ID Giày phải là một số nguyên hợp lệ.");
            }

            if (ModelState.IsValid)
            {
                db.Entry(tTGiay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tTGiay);
        }

        // GET: TTGiays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TTGiay tTGiay = db.TTGiays.Find(id);
            if (tTGiay == null)
            {
                return HttpNotFound();
            }
            return View(tTGiay);
        }

        // POST: TTGiays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TTGiay tTGiay = db.TTGiays.Find(id);
            db.TTGiays.Remove(tTGiay);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
