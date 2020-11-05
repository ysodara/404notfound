using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Peak_Performance.DAL;
using Peak_Performance.Models;
using Peak_Performance.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace Peak_Performance.Areas.Athlete.Controllers
{
    public class CurrentWorkoutController : Controller
    {
        private PeakPerformanceContext db = new PeakPerformanceContext();

        // GET: Athlete/CurrentWorkout
        public ActionResult Index()
        {
            return View();
        }

        // GET: Athlete/CurrentWorkout/Details/9
        public ActionResult Details(int? id) {
            if(id == null) {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            string userID = User.Identity.GetUserId();
            Person temp = db.Persons.FirstOrDefault(p => p.ASPNetIdentityID == userID);

            Workout currentWorkout = db.Workouts.Find(id);
            var completed = db.Records.Where(item => (item.WorkoutID == id) && (item.AthleteID == temp.ID)).Select(item => item.Completed);

            if(currentWorkout == null) {
                return HttpNotFound();
            }

            FullWorkoutViewModel viewModel = new FullWorkoutViewModel(currentWorkout.ID);

            viewModel.userID = temp.ID;

            if (db.Records.Any(m => m.WorkoutID == currentWorkout.ID)) {
                Record rec = db.Records.Where(m => m.WorkoutID == currentWorkout.ID).FirstOrDefault();
                ViewBag.Note = rec.Note;
            }

            return View(viewModel);
        }

        public ActionResult PDF(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            string userID = User.Identity.GetUserId();
            Person temp = db.Persons.FirstOrDefault(p => p.ASPNetIdentityID == userID);

            Workout currentWorkout = db.Workouts.Find(id);
            var completed = db.Records.Where(item => (item.WorkoutID == id) && (item.AthleteID == temp.ID)).Select(item => item.Completed);

            if (currentWorkout == null)
            {
                return HttpNotFound();
            }

            FullWorkoutViewModel viewModel = new FullWorkoutViewModel(currentWorkout.ID);

            if (db.Records.Any(m => m.WorkoutID == currentWorkout.ID))
            {
                Record rec = db.Records.Where(m => m.WorkoutID == currentWorkout.ID).FirstOrDefault();
                ViewBag.Note = rec.Note;
            }

            return View(viewModel);
        }

        public ActionResult DownloadPDF(int? Id)
        {
            var r = new Rotativa.ActionAsPdf("PDF", new { id = Id });

            return r;
        }
    }
}