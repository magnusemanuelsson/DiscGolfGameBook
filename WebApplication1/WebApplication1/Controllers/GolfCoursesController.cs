using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1;

namespace WebApplication1.Controllers
{
    public class GolfCoursesController : Controller
    {
        private Model1 db = new Model1();

        // GET: GolfCourses
        public ActionResult Index()
        {
            if (!Session["användare"].Equals("Admin"))
            {
                return RedirectToAction("Spela", "Home", null);
            }

            return View(db.GolfCourse.ToList());
        }

        // GET: GolfCourses/Details/5
        public ActionResult Details(int? id)
        {
            if (!Session["användare"].Equals("Admin"))
            {
                return RedirectToAction("Spela", "Home", null);
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GolfCourse golfCourse = db.GolfCourse.Find(id);
            if (golfCourse == null)
            {
                return HttpNotFound();
            }
            return View(golfCourse);
        }

        // GET: GolfCourses/Create
        public ActionResult Create()
        {
            if (!Session["användare"].Equals("Admin"))
            {
                return RedirectToAction("Spela", "Home", null);
            }

            return View();
        }

        // POST: GolfCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Location,Image")] GolfCourse golfCourse)
        {
            if (!Session["användare"].Equals("Admin"))
            {
                return RedirectToAction("Spela", "Home", null);
            }

            if (ModelState.IsValid)
            {
                db.GolfCourse.Add(golfCourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(golfCourse);
        }

        // GET: GolfCourses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!Session["användare"].Equals("Admin"))
            {
                return RedirectToAction("Spela", "Home", null);
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GolfCourse golfCourse = db.GolfCourse.Find(id);
            if (golfCourse == null)
            {
                return HttpNotFound();
            }
            return View(golfCourse);
        }

        // POST: GolfCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Location,Image")] GolfCourse golfCourse)
        {
            if (!Session["användare"].Equals("Admin"))
            {
                return RedirectToAction("Spela", "Home", null);
            }

            if (ModelState.IsValid)
            {
                db.Entry(golfCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(golfCourse);
        }

        // GET: GolfCourses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!Session["användare"].Equals("Admin"))
            {
                return RedirectToAction("Spela", "Home", null);
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GolfCourse golfCourse = db.GolfCourse.Find(id);
            if (golfCourse == null)
            {
                return HttpNotFound();
            }
            return View(golfCourse);
        }

        // POST: GolfCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!Session["användare"].Equals("Admin"))
            {
                return RedirectToAction("Spela", "Home", null);
            }

            GolfCourse golfCourse = db.GolfCourse.Find(id);
            db.GolfCourse.Remove(golfCourse);
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
