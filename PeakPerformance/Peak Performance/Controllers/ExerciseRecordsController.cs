using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Peak_Performance.DAL;
using Peak_Performance.Models;

namespace Peak_Performance.Controllers
{
    [Authorize(Roles = "Coach, Admin, Athlete")]
    public class ExerciseRecordsController : Controller
    {
        private PeakPerformanceContext db = new PeakPerformanceContext();

        // GET: ExerciseRecords
        public ActionResult Index()
        {
            var exerciseRecords = db.ExerciseRecords.Include(e => e.Athlete).Include(e => e.Exercis);
            return View(exerciseRecords.ToList());
        }

        // GET: ExerciseRecords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExerciseRecord exerciseRecord = db.ExerciseRecords.Find(id);
            if (exerciseRecord == null)
            {
                return HttpNotFound();
            }
            return View(exerciseRecord);
        }

        // GET: ExerciseRecords/Create
        public ActionResult Create()
        {
            ViewBag.AthleteID = new SelectList(db.Athletes, "ID", "Sex");
            ViewBag.ExerciseID = new SelectList(db.Exercises, "ID", "Name");
            return View();
        }

        // POST: ExerciseRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExerciseRecordId,ComplexReps,ComplexSets,LiftWeight,RunSpeed,RunTime,RunDistance,ExerciseID,AthleteID")] ExerciseRecord exerciseRecord)
        {
            if (ModelState.IsValid)
            {
                db.ExerciseRecords.Add(exerciseRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AthleteID = new SelectList(db.Athletes, "ID", "Sex", exerciseRecord.AthleteID);
            ViewBag.ExerciseID = new SelectList(db.Exercises, "ID", "Name", exerciseRecord.ExerciseID);
            return View(exerciseRecord);
        }

        // GET: ExerciseRecords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExerciseRecord exerciseRecord = db.ExerciseRecords.Find(id);
            if (exerciseRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.AthleteID = new SelectList(db.Athletes, "ID", "Sex", exerciseRecord.AthleteID);
            ViewBag.ExerciseID = new SelectList(db.Exercises, "ID", "Name", exerciseRecord.ExerciseID);
            return View(exerciseRecord);
        }

        // POST: ExerciseRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExerciseRecordId,ComplexReps,ComplexSets,LiftWeight,RunSpeed,RunTime,RunDistance,ExerciseID,AthleteID")] ExerciseRecord exerciseRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exerciseRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AthleteID = new SelectList(db.Athletes, "ID", "Sex", exerciseRecord.AthleteID);
            ViewBag.ExerciseID = new SelectList(db.Exercises, "ID", "Name", exerciseRecord.ExerciseID);
            return View(exerciseRecord);
        }

        // GET: ExerciseRecords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExerciseRecord exerciseRecord = db.ExerciseRecords.Find(id);
            if (exerciseRecord == null)
            {
                return HttpNotFound();
            }
            return View(exerciseRecord);
        }

        // POST: ExerciseRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExerciseRecord exerciseRecord = db.ExerciseRecords.Find(id);
            db.ExerciseRecords.Remove(exerciseRecord);
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
