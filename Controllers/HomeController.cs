using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using ContosoUniversity.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();
        public ActionResult Index(int? id_geo, int? id_pro, int? id_am, int? id_tom, bool? isbool)
        {
            var geo = from s in db.Geographies select s;
            List<Geography> geography = geo.ToList();
            SelectList selectListItems_geo = new SelectList(geography, "id", "name");
            ViewBag.selectListItem_geo = selectListItems_geo;

            //Geography _geography = new Geography();
            if (id_geo != null)
            {
 
                //_geography = db.Geographies.Find(id_geo);
                var pro = from s in db.Provinces where s.geography_id == id_geo select s;
                List<Province> provinces = pro.ToList();

                //Province _province = new Province();
                if (id_pro != null)
                {
                    SelectList selectListItems_pro = new SelectList(provinces, "id", "name_th", id_pro);
                    ViewBag.selectListItem_province = selectListItems_pro;
                    //_province = db.Provinces.Find(id_pro);

                    var am = from s in db.Amphures where s.province_id == id_pro select s;
                    List<Amphure> amphures = am.ToList();


                    if (id_am != null)
                    {
                        SelectList selectListItems_am = new SelectList(amphures, "id", "name_th",id_am);
                        ViewBag.selectListItem_amphure = selectListItems_am;

                        var tom = from s in db.Tumbons where s.amphure_id == id_am select s;
                        List<Tumbon> tumbons = tom.ToList();
                        
                        if (id_tom != null)
                        {
                            SelectList selectListItems_tom = new SelectList(tumbons, "id", "name_th",id_tom);
                            ViewBag.selectListItem_tumbon = selectListItems_tom;
                        }else
                        {

                            SelectList selectListItems_tom = new SelectList(tumbons, "id", "name_th");
                            ViewBag.selectListItem_tumbon = selectListItems_tom;
                        }

                    }
                    else
                    {
                        SelectList selectListItems_am = new SelectList(amphures, "id", "name_th");
                        ViewBag.selectListItem_amphure = selectListItems_am;

                    }
                }
                else
                {
                    SelectList selectListItems_pro = new SelectList(provinces, "id", "name_th");
                    ViewBag.selectListItem_province = selectListItems_pro;

                }


            }
            return View();
        }

        public ActionResult About()
        {
            IQueryable<EnrollmentDateGroup> data = from employee in db.employees
                                                   group employee by employee.createdOn into dateGroup
                                                   select new EnrollmentDateGroup()
                                                   {
                                                       EnrollmentDate = dateGroup.Key,
                                                       StudentCount = dateGroup.Count()
                                                   };

            return View(data.ToList());
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}