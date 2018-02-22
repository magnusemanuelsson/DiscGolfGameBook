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
    public class GameRoundsController : Controller
    {
        private Model1 db = new Model1();

        // GET: GameRounds
        public ActionResult Index()
        {
            var gameRound = db.GameRound.Include(g => g.Game1).Include(g => g.Hole1);
            return View(gameRound.ToList());
        }

        // GET: GameRounds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameRound gameRound = db.GameRound.Find(id);
            if (gameRound == null)
            {
                return HttpNotFound();
            }
            return View(gameRound);
        }

        // GET: GameRounds/Create
        public ActionResult Create()
        {
            ViewBag.Game = new SelectList(db.Game, "ID", "ID");
            ViewBag.Hole = new SelectList(db.Hole, "ID", "ID");
            return View();
        }

        // POST: GameRounds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Game,Hole,Throws")] GameRound gameRound)
        {
            if (ModelState.IsValid)
            {
                db.GameRound.Add(gameRound);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Game = new SelectList(db.Game, "ID", "ID", gameRound.Game);
            ViewBag.Hole = new SelectList(db.Hole, "ID", "ID", gameRound.Hole);
            return View(gameRound);
        }

        // GET: GameRounds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameRound gameRound = db.GameRound.Find(id);
            if (gameRound == null)
            {
                return HttpNotFound();
            }
            ViewBag.Game = new SelectList(db.Game, "ID", "ID", gameRound.Game);
            ViewBag.Hole = new SelectList(db.Hole, "ID", "ID", gameRound.Hole);
            return View(gameRound);
        }

        // POST: GameRounds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Game,Hole,Throws")] GameRound gameRound)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gameRound).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Game = new SelectList(db.Game, "ID", "ID", gameRound.Game);
            ViewBag.Hole = new SelectList(db.Hole, "ID", "ID", gameRound.Hole);
            return View(gameRound);
        }

        // GET: GameRounds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameRound gameRound = db.GameRound.Find(id);
            if (gameRound == null)
            {
                return HttpNotFound();
            }
            return View(gameRound);
        }

        // POST: GameRounds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GameRound gameRound = db.GameRound.Find(id);
            db.GameRound.Remove(gameRound);
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
