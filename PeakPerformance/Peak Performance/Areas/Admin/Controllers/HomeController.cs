using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Peak_Performance.DAL;
using Peak_Performance.Models;

namespace Peak_Performance.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private PeakPerformanceContext db = new PeakPerformanceContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllTeams()
        {
            var teams = db.Teams.ToList();
            return View(teams);
        }

        public ActionResult AllAthletes()
        {
            var athletes = db.Athletes.ToList();
            return View(athletes);
        }

        public ActionResult AllCoaches()
        {
            var coaches = db.Coaches.ToList();

            return View(coaches);
        }

        public ActionResult AllAdmin()
        {
            var adminRoles = db.AspNetRoles.Where(r => r.Id == "1").Select(r => r.AspNetUsers).ToList()[0];
            var checkIfAnyExist = RecordCheck(adminRoles.Count);
            if (checkIfAnyExist == false)
            {
                return View("Error");
            }
            return View(adminRoles);
        }

        public bool RecordCheck(int count)
        {
            if (count <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}