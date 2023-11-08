using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AmphuresController : Controller
    {
        private Entities db = new Entities();

        // GET: Amphures
        public ActionResult Index()
        {
            var amphures = db.Amphures.Include(a => a.Province);
            return View(amphures.ToList());
        }

        // GET: Amphures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Amphure amphure = db.Amphures.Find(id);
            if (amphure == null)
            {
                return HttpNotFound();
            }
            return View(amphure);
        }

        // GET: Amphures/Create
        public ActionResult Create()
        {
            ViewBag.province_id = new SelectList(db.Provinces, "id", "name_th");
            return View();
        }

        // POST: Amphures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name_th,name_en,province_id,created_at,updated_at,deleted_at")] Amphure amphure)
        {
            if (ModelState.IsValid)
            {
                db.Amphures.Add(amphure);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.province_id = new SelectList(db.Provinces, "id", "name_th", amphure.province_id);
            return View(amphure);
        }

        // GET: Amphures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Amphure amphure = db.Amphures.Find(id);
            if (amphure == null)
            {
                return HttpNotFound();
            }
            ViewBag.province_id = new SelectList(db.Provinces, "id", "name_th", amphure.province_id);
            return View(amphure);
        }

        // POST: Amphures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name_th,name_en,province_id,created_at,updated_at,deleted_at")] Amphure amphure)
        {
            if (ModelState.IsValid)
            {
                db.Entry(amphure).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.province_id = new SelectList(db.Provinces, "id", "name_th", amphure.province_id);
            return View(amphure);
        }

        // GET: Amphures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Amphure amphure = db.Amphures.Find(id);
            if (amphure == null)
            {
                return HttpNotFound();
            }
            return View(amphure);
        }

        // POST: Amphures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Amphure amphure = db.Amphures.Find(id);
            db.Amphures.Remove(amphure);
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
