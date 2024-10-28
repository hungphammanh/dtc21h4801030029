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
    public class GiayChiTiet1Controller : Controller
    {
        private BanGiay1Entities db = new BanGiay1Entities();

        // GET: GiayChiTiet1
        public ActionResult Index()
        {
            var giayChiTiet1 = db.GiayChiTiet1.Include(g => g.TTGiay);
            return View(giayChiTiet1.ToList());
        }

        public ActionResult SearchByTenGiay(string tenGiay)
        {
            if (string.IsNullOrEmpty(tenGiay))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var giayChiTiet1 = db.GiayChiTiet1.Include(g => g.TTGiay)
                                              .Where(g => g.TenGiay.Contains(tenGiay))
                                              .ToList();

            if (giayChiTiet1 == null || giayChiTiet1.Count == 0)
            {
                return HttpNotFound();
            }

            return View("Index", giayChiTiet1);
        }

        // GET: GiayChiTiet1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiayChiTiet1 giayChiTiet1 = db.GiayChiTiet1.Find(id);
            if (giayChiTiet1 == null)
            {
                return HttpNotFound();
            }
            return View(giayChiTiet1);
        }

        // GET: GiayChiTiet1/Create
        public ActionResult Create()
        {
            ViewBag.idGiay = new SelectList(db.TTGiays, "idGiay", "LoaiGiay");
            return View();
        }


        // POST: GiayChiTiet1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTT,TenGiay,Mau,Soluong,idGiay")] GiayChiTiet1 giayChiTiet1)
        {
            if (!int.TryParse(giayChiTiet1.Soluong.ToString(), out int soluong))
            {
                ModelState.AddModelError("Soluong", "Số lượng phải là một số nguyên hợp lệ.");
            }

            if (ModelState.IsValid)
            {
                db.GiayChiTiet1.Add(giayChiTiet1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idGiay = new SelectList(db.TTGiays, "idGiay", "LoaiGiay", giayChiTiet1.idGiay);
            return View(giayChiTiet1);
        }


        // GET: GiayChiTiet1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiayChiTiet1 giayChiTiet1 = db.GiayChiTiet1.Find(id);
            if (giayChiTiet1 == null)
            {
                return HttpNotFound();
            }
            ViewBag.idGiay = new SelectList(db.TTGiays, "idGiay", "LoaiGiay", giayChiTiet1.idGiay);
            return View(giayChiTiet1);
        }


        // POST: GiayChiTiet1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTT,TenGiay,Mau,Soluong,idGiay")] GiayChiTiet1 giayChiTiet1)
        {
            if (!int.TryParse(giayChiTiet1.Soluong.ToString(), out int soluong))
            {
                ModelState.AddModelError("Soluong", "Số lượng phải là một số nguyên hợp lệ.");
            }

            if (ModelState.IsValid)
            {
                db.Entry(giayChiTiet1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idGiay = new SelectList(db.TTGiays, "idGiay", "LoaiGiay", giayChiTiet1.idGiay);
            return View(giayChiTiet1);
        }

        // GET: GiayChiTiet1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiayChiTiet1 giayChiTiet1 = db.GiayChiTiet1.Find(id);
            if (giayChiTiet1 == null)
            {
                return HttpNotFound();
            }
            return View(giayChiTiet1);
        }

        // POST: GiayChiTiet1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GiayChiTiet1 giayChiTiet1 = db.GiayChiTiet1.Find(id);
            db.GiayChiTiet1.Remove(giayChiTiet1);
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
