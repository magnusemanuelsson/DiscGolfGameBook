﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Newtonsoft.Json;
using WebApplication1;
using WebApplication1.Models;
using TweetSharp;
using System.Timers;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private Model1 db = new Model1();
        private static string customer_key = "naXZJ2S2sq77gv9hbhYxOg7S0";
        private static string customer_key_secret = "ESccotWGpj17QFKUmL7ao5JQOfm8DCUdbQuh1ONtlf6Iq2Kkq7";
        private static string access_token = "999651100587384832-IzUDZ3nP73HD5ckcoDJtdmxBbvkCe0c";
        private static string access_token_secret = "o2KnqDprkA9yljzGshzVtAZbjO0cI1iyRnkqOInfScxkT";
          
        private static TwitterService service = new TwitterService(customer_key, customer_key_secret, access_token, access_token_secret);

        private static void SendTweet(string _status)
        {
            service.SendTweet(new SendTweetOptions { Status = _status }, (tweet, response) => 
                {
                if(response.StatusCode == HttpStatusCode.OK)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"<{DateTime.Now}> - Tweet Sent!");
                        Console.ResetColor();
                }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"<ERROR> " + response.Error.Message);
                        Console.ResetColor();
                    }
            });
        }

        public ActionResult Index()
        {

            if (Session["användarID"] == null)
            {
                return Redirect("/Login.aspx");
            }
            return RedirectToAction("Spela");
        }
        
        public ActionResult Spela(string searchString)
        {

            WeatherInfo.RootObject wi = new WeatherInfo.RootObject();

            wi = GetWeather();

            ViewBag.temp = wi.timeSeries[2].parameters[1].values[0];
            ViewBag.vind = wi.timeSeries[2].parameters[4].values[0];
            ViewBag.tid = wi.timeSeries[2].validTime;

            if (Session["användarID"] == null)
            {
                return Redirect("/Login.aspx");
            }
            int userID = Int32.Parse(Session["användarID"].ToString());
            if ((from s in db.GameRound where s.Game1.Active == 1 && s.Game1.Player == userID select s).FirstOrDefault() != null)
            {
                int unfinishedID = ((from s in db.GameRound where s.Game1.Active == 1 && s.Game1.Player == userID select s).OrderBy(o => o.Hole1.Number)).FirstOrDefault().ID;
                return RedirectToAction("PlayRound", new { id = unfinishedID });
            }
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
            

            if (Session["användarID"] == null)
            {
                return Redirect("/Login.aspx");
            }
            Game newGame = new Game();
            int gameID = 0;

            String courseId = collection["Course"];
            int IdCourse = Int32.Parse(courseId);

            string userID = Session["användarID"].ToString();
            int IdUser = Int32.Parse(userID);

            newGame.GolfCourse = IdCourse;
            newGame.Player = IdUser;

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

            return RedirectToAction("PlayRound", new { id = startRoundID });



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
            if (Session["användarID"] == null)
            {
                return Redirect("/Login.aspx");
            }
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
            if (Session["användarID"] == null)
            {
                return Redirect("/Login.aspx");
            }
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
                        nextRoundID = ((from s in db.GameRound where s.Game1.ID == gameID select s).OrderBy(o => o.Hole1.Number)).FirstOrDefault().ID;
                    }
                    else
                    {
                        nextRoundID = ((from s in db.GameRound where s.Game1.ID == gameID select s).OrderByDescending(o => o.Hole1.Number)).FirstOrDefault().ID;
                    }
                }

                return RedirectToAction("PlayRound", new { id = nextRoundID });
            }



            ViewBag.Game = new SelectList(db.Game, "ID", "ID", gameRound.Game);
            ViewBag.Hole = new SelectList(db.Hole, "ID", "ID", gameRound.Hole);

            return View(gameRound);
        }
     

        public ActionResult FinishRound(int id, FormCollection fc)
        {
            
            if (Session["användarID"] == null)
            {
                return Redirect("/Login.aspx");
            }
            var gameRounds = from s in db.GameRound where s.Game1.ID == id select s;

            foreach(var item in gameRounds)
            {
                if(item.Throws == 0)
                {
                    int delID = item.ID;
                    GameRound gr = db.GameRound.Find(delID);
                    db.GameRound.Remove(gr);
                    
                }
            }
            db.SaveChanges();

            gameRounds = from s in db.GameRound where s.Game1.ID == id select s;

            Game game = new Game();
            game = db.Game.Find(id);
            Session["currentID"] = id;

            //int idGame = gameRound.Game;
            var TotalThrows = (from o in db.GameRound where o.Game == id select o.Throws).ToList();
            var TotalPar = (from x in db.GameRound where x.Game == id select x.Hole1.Par).ToList();

            game.Total_Par = (TotalThrows.Sum() - TotalPar.Sum());

        


            game.Date = DateTime.Today;
            game.Active = 0;
            db.Entry(game).State = EntityState.Modified;
            db.SaveChanges();

            ViewBag.totalscore = TotalThrows.Sum();
            ViewBag.date = game.Date.Value.ToString("yyyy/MM/dd");
            if (fc["dela"] != null)
            {
                SendTweet("Jag fick nytt rekord +" + game.Total_Par.ToString());
                return View(gameRounds.ToList());

            }

            return View(gameRounds.ToList());
        }

        /*[HttpPost]
        public ActionResult FinishRound(FormCollection result)
        {
            
            return RedirectToAction("Stats", new { id = startRoundID }); ;
        }*/
        public ActionResult Stats()
        {
            if (Session["användarID"] == null)
            {
                return Redirect("/Login.aspx");
            }
            string userID = Session["användarID"].ToString();
            int IDuser = Int32.Parse(userID);

            var gameForPlayer = from s in db.Game where s.Player == IDuser && s.Active == 0 select s;

            ViewBag.Userid = userID;
            return View(gameForPlayer.ToList());
        }

        public ActionResult LogOut()
        {
            if (Session["användarID"] == null)
            {
                return Redirect("/Login.aspx");
            }
            Session.Abandon();
            return RedirectToAction("Index");
        }

        public ActionResult Account()
        {
            if (Session["användarID"] == null)
            {
                return Redirect("/Login.aspx");
            }
            int id = Int32.Parse(Session["användarID"].ToString());
            Player player = db.Player.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Account([Bind(Include = "ID,Name,Username,Password,TotalScore")] Player player)
        {
            if (ModelState.IsValid)
            {
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.pwsaved = "Password successfully saved!";
            }
            return View(player);
        }

        public ActionResult Admin()
        {
            if (Session["användarID"] == null)
            {
                return Redirect("/Login.aspx");
            }
            if (!Session["användare"].Equals("Admin"))
            {
                return RedirectToAction("Spela");
            }
            return View();
        }
        public ActionResult Weather()
        {
            WeatherInfo.RootObject wi = new WeatherInfo.RootObject();
            
            wi = GetWeather();
         
                ViewBag.temp = wi.timeSeries[1].parameters[1].values[0];
                ViewBag.dag = wi.timeSeries[1].parameters[4].values[0];
                ViewBag.tid = wi.timeSeries[1].validTime;
                return View(wi.timeSeries.ToList());
        }
        
        public WeatherInfo.RootObject GetWeather()
        {
            string url = "https://opendata-download-metfcst.smhi.se/api/category/pmp3g/version/2/geotype/point/lon/20.263035/lat/63.825847/data.json";
            WebClient webClient = new WebClient() { Encoding = Encoding.UTF8 };
            string result = webClient.DownloadString(url);
            WeatherInfo.RootObject weatherInfo = new WeatherInfo.RootObject();
            weatherInfo = JsonConvert.DeserializeObject<WeatherInfo.RootObject>(result);

            return weatherInfo;
        }

    }
}