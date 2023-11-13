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
    public class AddressesController : Controller
    {
        private Entities db = new Entities();

        // GET: Addresses
        public ActionResult Index()
        {
            var addresses = from s in db.Addresses where s.isdeleted == false select s;
            return View(addresses);
        }

        // GET: Addresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }
        public ActionResult AddResident(int? id)
        {

            return RedirectToAction("Create");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddResident(List<Resident> residents)
        {

            return View();
        }


        // GET: Addresses/Create
        public ActionResult Create()
        {
            //List<Address> addresses = new List<Address>();
            AddressMain addressMain = new AddressMain();
            addressMain.addressList = new List<Address>();
            addressMain.addressList.Add(new Address());

            return View(addressMain);
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(AddressMain addressMain, bool isadd, bool isdelete, bool isAddResident)
        {
            foreach (var item in addressMain.addressList)
            {
                if (item.Residents.Where(s => s.ideleted == true).Count() > 0)
                {
                    item.Residents = item.Residents.Where(s => s.ideleted != true).ToList();
                    ModelState.Clear();
                    return View(addressMain);
                }
            }
            if (isadd) //isbool = false => addform
            {
                if (isAddResident)
                {
                    foreach (var item in addressMain.addressList)
                    {
                        if (item.isaddres == true)
                        {
                            item.Residents.Add(new Resident());
                            //item.isaddres = false;
                        }
                    }
                    return View(addressMain);
                }
                else
                {
                    if (addressMain.addressList != null)
                    {
                        addressMain.addressList.Add(new Address());
                        return View(addressMain);
                    }
                    else
                    {
                        addressMain.addressList = new List<Address>();
                        addressMain.addressList.Add(new Address());
                        return View(addressMain);

                    }
                }
            }
            else //sent to database
            {
                if (isdelete)
                {
                    if (addressMain.addressList != null)
                    {
                        addressMain.addressList.RemoveAt(addressMain.addressList.Count - 1);
                        return View(addressMain);
                    }
                    else
                    {
                        return View(addressMain);
                    }
                }
                else
                {
                    foreach (var i in addressMain.addressList)
                    {
                        i.isdeleted = false;
                        db.Addresses.Add(i);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
            }

        }

        // GET: Addresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,firstname,middlename,lastname,street,city,zipcode,isdeleted")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(address);
        }

        // GET: Addresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Address address = db.Addresses.Find(id);
            //db.Addresses.Remove(address);
            address.isdeleted = true;
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
