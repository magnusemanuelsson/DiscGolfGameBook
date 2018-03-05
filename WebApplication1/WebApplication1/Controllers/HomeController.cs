using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
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
            //ViewBag.användare = Session["användare"].ToString();
            return View(selectCourse);
        }

        [HttpPost]
        public ActionResult Spela(FormCollection collection)
        {
            Game newGame = new Game();
            int gameID = 0;

            String courseId = collection["Course"];
            int IdCourse = Int32.Parse(courseId);
            
            newGame.GolfCourse = IdCourse;
            newGame.Player = 1;

            if (ModelState.IsValid)
            {
                db.Game.Add(newGame);
                db.SaveChanges();
                gameID = newGame.ID;
            }

            var holes = (from s in db.Hole
                           where s.GolfCourse1.ID == IdCourse
                           select new
                           {
                               id = s.ID,
                           }).ToArray();
            Debug.WriteLine("holes = " + holes.Length);
            int i = 0;
            foreach (var element in holes)
            {
                GameRound gameround = new GameRound();
                gameround.Game = newGame.ID;
                gameround.Hole = element.id;
                Debug.WriteLine("Hole ID = " + element.id);
                gameround.Throws = 0;
                i++;
                if (ModelState.IsValid)
                {
                    db.GameRound.Add(gameround);
                    db.SaveChanges();
                }
            }

            int startRoundID = (from s in db.GameRound where s.Game1.ID == gameID && s.Hole1.Number == 1 select s).First().ID;

            return RedirectToAction("PlayRound" , new { id = startRoundID });
            
 
        
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

        public ActionResult PlayRound(int? id)
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
            this.Session["currentID"] = id;
            ViewBag.gameID = (from r in db.GameRound where r.ID == id select r.Game).First();
            return View(gameRound);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlayRound([Bind(Include = "ID,Game,Hole,Throws")] GameRound gameRound)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gameRound).State = EntityState.Modified;
                db.SaveChanges();
                int id = (int)HttpContext.Session["currentID"];
                int currentHoleNumber = (from r in db.GameRound where r.ID == id select r.Hole1.Number).First();
                int gameID = (from r in db.GameRound where r.ID == id select r.Game).First();
                bool next = false;
                if (Request.Form["previousbutton"] != null)
                {
                    currentHoleNumber--;
                }
                else if (Request.Form["nextbutton"] != null)
                {
                    currentHoleNumber++;
                    next = true;
                }
                int nextRoundID;
                if ((from s in db.GameRound where s.Game1.ID == gameID && s.Hole1.Number == currentHoleNumber select s).FirstOrDefault() != null)
                {
                    nextRoundID = (from s in db.GameRound where s.Game1.ID == gameID && s.Hole1.Number == currentHoleNumber select s).FirstOrDefault().ID;
                }
                else
                {
                    if (next)
                    {
                        nextRoundID = (from s in db.GameRound where s.Game1.ID == gameID && s.Hole1.Number == 1 select s).FirstOrDefault().ID;
                    }
                    else
                    {
                        nextRoundID = (from s in db.GameRound where s.Game1.ID == gameID select s).LastOrDefault().ID;
                    }
                }
                return RedirectToAction("PlayRound", new { id = nextRoundID });
            }
            ViewBag.Game = new SelectList(db.Game, "ID", "ID", gameRound.Game);
            ViewBag.Hole = new SelectList(db.Hole, "ID", "ID", gameRound.Hole);
            return View(gameRound);
        }
    }
}