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
    public class TumbonsController : Controller
    {
        private Entities db = new Entities();

        // GET: Tumbons
        public ActionResult Index()
        {
            var tumbons = db.Tumbons.Include(t => t.Amphure);
            return View(tumbons.ToList());
        }

        // GET: Tumbons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tumbon tumbon = db.Tumbons.Find(id);
            if (tumbon == null)
            {
                return HttpNotFound();
            }
            return View(tumbon);
        }

        // GET: Tumbons/Create
        public ActionResult Create()
        {
            ViewBag.amphure_id = new SelectList(db.Amphures, "id", "name_th");
            return View();
        }

        // POST: Tumbons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,zip_code,name_th,name_en,amphure_id,created_at,updated_at,deleted_at")] Tumbon tumbon)
        {
            if (ModelState.IsValid)
            {
                db.Tumbons.Add(tumbon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.amphure_id = new SelectList(db.Amphures, "id", "name_th", tumbon.amphure_id);
            return View(tumbon);
        }

        // GET: Tumbons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tumbon tumbon = db.Tumbons.Find(id);
            if (tumbon == null)
            {
                return HttpNotFound();
            }
            ViewBag.amphure_id = new SelectList(db.Amphures, "id", "name_th", tumbon.amphure_id);
            return View(tumbon);
        }

        // POST: Tumbons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,zip_code,name_th,name_en,amphure_id,created_at,updated_at,deleted_at")] Tumbon tumbon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tumbon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.amphure_id = new SelectList(db.Amphures, "id", "name_th", tumbon.amphure_id);
            return View(tumbon);
        }

        // GET: Tumbons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tumbon tumbon = db.Tumbons.Find(id);
            if (tumbon == null)
            {
                return HttpNotFound();
            }
            return View(tumbon);
        }

        // POST: Tumbons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tumbon tumbon = db.Tumbons.Find(id);
            db.Tumbons.Remove(tumbon);
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
