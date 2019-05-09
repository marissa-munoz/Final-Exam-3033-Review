/*
 * Marissa Munoz
 */
using MVC_Problem2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Problem2.Controllers
{
    public class ScheduleController : Controller
    {
        private DB_128040_practiceEntities db = new DB_128040_practiceEntities();
        // GET: Schedule
        public ActionResult Index(int? year)
        {
            if(year == null)
            {
                year = DateTime.Now.Year;
            }
            var games = db.FootballSchedules.Where(s => s.Season == year.Value);

            //var g = new List<FootballSchedule>();

            //foreach (var game in db.FootballSchedules)
            //{
            //    if(game.Season == year.Value)
            //    {
            //        g.Add(game);
            //    }
            //}
            return View(games);
        }
    }
}