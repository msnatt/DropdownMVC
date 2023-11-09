using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class FileTablesController : Controller
    {
        private Entities db = new Entities();

        // GET: FileTables
        public ActionResult Index()
        {
            var _filetable = from s in db.FileTables select s;

            _filetable = _filetable.Where(s => s.isdeleted == false);

            return View(_filetable);
        }

        // GET: FileTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileTable fileTable = db.FileTables.Find(id);
            if (fileTable == null)
            {
                return HttpNotFound();
            }
            return View(fileTable);
        }

        // GET: FileTables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FileTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "id,namefile,path,isdeleted")] FileTable fileTable)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.FileTables.Add(fileTable);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(fileTable);
        //}

        // GET: FileTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileTable fileTable = db.FileTables.Find(id);
            if (fileTable == null)
            {
                return HttpNotFound();
            }

            return View(fileTable);
        }

        // POST: FileTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,namefile,path,isdeleted")] FileTable fileTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fileTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fileTable);
        }

        // GET: FileTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileTable fileTable = db.FileTables.Find(id);
            if (fileTable == null)
            {
                return HttpNotFound();
            }
            return View(fileTable);
        }

        // POST: FileTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FileTable fileTable = db.FileTables.Find(id);
            //db.FileTables.Remove(fileTable);
            fileTable.isdeleted = true;
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
        [HttpGet]
        public ActionResult FileTablecontact()
        {
            return View();
        }


        [HttpPost]
        public ActionResult FileTablecontact([Bind(Include = "id,name,namefile,path,isdeleted,createOn,ImageFile")] FileTable fileTable)
        {
            foreach (var file in fileTable.ImageFile)
            {
                string FileName = Path.GetFileName(file.FileName);

                FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName;

                string UploadPath = Path.Combine(Server.MapPath("~/UserImages"), FileName);

                file.SaveAs(UploadPath);
                fileTable.namefile = FileName;
                fileTable.path = "~/UserImages/";
                fileTable.isdeleted = false;
                fileTable.createOn = DateTime.Now;
                db.FileTables.Add(fileTable);

                db.SaveChanges();
            }

            return RedirectToAction("Create");

        }
        public ActionResult Download(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            FileTable fileTable = db.FileTables.Find(id);
            if (fileTable == null)
            {
                return HttpNotFound();
            }
            var FileVirtualPath = fileTable.path + fileTable.namefile;
            var Pathfile = Path.GetFileName(FileVirtualPath);

            var dir = new DirectoryInfo(Server.MapPath(fileTable.path));
            FileInfo[] fileNames = dir.GetFiles("*.*");

            List<string> items = new List<string>();
            foreach (var file in fileNames)
            {
                items.Add(file.Name);
            }
            if (!items.Contains(fileTable.namefile))
            {
                return HttpNotFound();
            }
            else
            {
                return File(FileVirtualPath, "application/force-download", Pathfile);
            }
        }

        private object DirectoryInfo(string v)
        {
            throw new NotImplementedException();
        }
    }
}
