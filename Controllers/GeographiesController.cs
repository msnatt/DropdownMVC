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
    public class GeographiesController : Controller
    {
        private Entities db = new Entities();

        // GET: Geographies
        public ActionResult Index()
        {
            return View(db.Geographies.ToList());
        }

        // GET: Geographies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Geography geography = db.Geographies.Find(id);
            if (geography == null)
            {
                return HttpNotFound();
            }
            return View(geography);
        }

        // GET: Geographies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Geographies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] Geography geography)
        {
            if (ModelState.IsValid)
            {
                db.Geographies.Add(geography);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(geography);
        }

        // GET: Geographies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Geography geography = db.Geographies.Find(id);
            if (geography == null)
            {
                return HttpNotFound();
            }
            return View(geography);
        }

        // POST: Geographies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] Geography geography)
        {
            if (ModelState.IsValid)
            {
                db.Entry(geography).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(geography);
        }

        // GET: Geographies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Geography geography = db.Geographies.Find(id);
            if (geography == null)
            {
                return HttpNotFound();
            }
            return View(geography);
        }

        // POST: Geographies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Geography geography = db.Geographies.Find(id);
            db.Geographies.Remove(geography);
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
