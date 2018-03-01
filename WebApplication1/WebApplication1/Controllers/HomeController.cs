using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private Model1 db = new Model1();

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Spela(string searchString)
        {
            SelectCourse selectCourse = new SelectCourse();
            selectCourse.Courses = db.GolfCourse.ToList();
            selectCourse.Location = new SelectList(db.GolfCourse.GroupBy(x => x.Location).Select(x => x.FirstOrDefault()), "Location", "Location");
            selectCourse.Courses = db.GolfCourse.ToList();
            selectCourse.Name = new SelectList(db.GolfCourse, "ID", "Name");

            var golfCourses = from s in db.GolfCourse select s;
            if (!String.IsNullOrEmpty(searchString)) { golfCourses = golfCourses.Where(s => s.Location.Contains(searchString)); }

            ViewBag.Locations = new SelectList(db.GolfCourse, "Name", "Location");

            return View(selectCourse);
        }

        [HttpPost]
        public ActionResult Spela(FormCollection collection)
        {
            Session["mittdata"] += collection["LocationDD"].ToString();
            //Response.Redirect("~/Home/About");

            return this.RedirectToAction("Index" , "");
        }

        [HttpPost]
        public ActionResult CoursesByLocation(string locationName)//SelectCourse selectCourse, FormCollection form, string locationName)
        {
            var courses = (from s in db.GolfCourse
                          where s.Location.Equals(locationName)
                           select new
                          {
                              id = s.ID,
                              course = s.Name
                          }).ToArray();

            return Json(courses, JsonRequestBehavior.AllowGet);
        }
    }
}