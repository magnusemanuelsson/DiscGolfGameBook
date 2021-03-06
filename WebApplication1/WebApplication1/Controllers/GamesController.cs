﻿using System;
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
    public class GamesController : Controller
    {
        private Model1 db = new Model1();

        // GET: Games
        public ActionResult Index()
        {
            if (!Session["användare"].Equals("Admin"))
            {
                return RedirectToAction("Spela", "Home", null);
            }

            var game = db.Game.Include(g => g.GolfCourse1).Include(g => g.Player1);
            return View(game.ToList());
        }

        // GET: Games/Details/5
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
            Game game = db.Game.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            if (!Session["användare"].Equals("Admin"))
            {
                return RedirectToAction("Spela", "Home", null);
            }

            ViewBag.GolfCourse = new SelectList(db.GolfCourse, "ID", "Name");
            ViewBag.Player = new SelectList(db.Player, "ID", "Name");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Player,GolfCourse,Date,Total_Par,Active")] Game game)
        {
            if (!Session["användare"].Equals("Admin"))
            {
                return RedirectToAction("Spela", "Home", null);
            }

            if (ModelState.IsValid)
            {
                db.Game.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GolfCourse = new SelectList(db.GolfCourse, "ID", "Name", game.GolfCourse);
            ViewBag.Player = new SelectList(db.Player, "ID", "Name", game.Player);
            return View(game);
        }

        // GET: Games/Edit/5
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
            Game game = db.Game.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.GolfCourse = new SelectList(db.GolfCourse, "ID", "Name", game.GolfCourse);
            ViewBag.Player = new SelectList(db.Player, "ID", "Name", game.Player);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Player,GolfCourse,Date,Total_Par,Active")] Game game)
        {
            if (!Session["användare"].Equals("Admin"))
            {
                return RedirectToAction("Spela", "Home", null);
            }

            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GolfCourse = new SelectList(db.GolfCourse, "ID", "Name", game.GolfCourse);
            ViewBag.Player = new SelectList(db.Player, "ID", "Name", game.Player);
            return View(game);
        }

        // GET: Games/Delete/5
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
            Game game = db.Game.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!Session["användare"].Equals("Admin"))
            {
                return RedirectToAction("Spela", "Home", null);
            }

            Game game = db.Game.Find(id);
            db.Game.Remove(game);
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
