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
    public class HolesController : Controller
    {
        private Model1 db = new Model1();

        // GET: Holes
        public ActionResult Index()
        {
            var hole = db.Hole.Include(h => h.GolfCourse1);
            return View(hole.ToList());
        }

        // GET: Holes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hole hole = db.Hole.Find(id);
            if (hole == null)
            {
                return HttpNotFound();
            }
            return View(hole);
        }

        // GET: Holes/Create
        public ActionResult Create()
        {
            ViewBag.GolfCourse = new SelectList(db.GolfCourse, "ID", "Name");
            return View();
        }

        // POST: Holes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,GolfCourse,Par,Number")] Hole hole)
        {
            if (ModelState.IsValid)
            {
                db.Hole.Add(hole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GolfCourse = new SelectList(db.GolfCourse, "ID", "Name", hole.GolfCourse);
            return View(hole);
        }

        // GET: Holes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hole hole = db.Hole.Find(id);
            if (hole == null)
            {
                return HttpNotFound();
            }
            ViewBag.GolfCourse = new SelectList(db.GolfCourse, "ID", "Name", hole.GolfCourse);
            return View(hole);
        }

        // POST: Holes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,GolfCourse,Par,Number")] Hole hole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GolfCourse = new SelectList(db.GolfCourse, "ID", "Name", hole.GolfCourse);
            return View(hole);
        }

        // GET: Holes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hole hole = db.Hole.Find(id);
            if (hole == null)
            {
                return HttpNotFound();
            }
            return View(hole);
        }

        // POST: Holes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hole hole = db.Hole.Find(id);
            db.Hole.Remove(hole);
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
