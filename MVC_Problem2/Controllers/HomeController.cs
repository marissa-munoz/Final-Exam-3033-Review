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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About the university of oklahoma football program";

            About a = new About();

            return View(a);
        }
        
    }
}