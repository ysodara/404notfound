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
using Microsoft.AspNet.Identity;
using System.Reflection;
using Peak_Performance.Models.ViewModels;
using System.Net.Mail;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Peak_Performance.Areas.Coach
{
    [Authorize(Roles = "Coach")]
    public class WorkoutsController : Controller
    {

        private PeakPerformanceContext db = new PeakPerformanceContext();

        // GET: Coach/Workouts
        public ActionResult Index()
        {
            var workouts = db.Workouts.Include(w => w.Team);
            return View(workouts.ToList());
        }
        public ActionResult SearchMain()
        {
            string id = User.Identity.GetUserId();
            Peak_Performance.Models.Coach temp = db.Coaches.FirstOrDefault(p => p.Person.ASPNetIdentityID == id);
            ViewBag.MuscleGroupsId = new SelectList(db.MuscleGroups, "ID", "Name");
            ViewBag.TeamList = new SelectList(db.Teams.Where(t => t.CoachID == temp.ID), "ID", "TeamName");
            return View();

        }
        //[RequireRouteValues(new[] { "TeamList", "Date" })]
        //public JsonResult CreateWorkout(int TeamList, string Date)

        [HttpPost]
        public ActionResult CreateWorkout(WorkoutsViewModel workoutsViewModel)
        {
            try
            {
                string id = User.Identity.GetUserId();
                //Peak_Performance.Models.Workout myworkout = new Workout();
                Peak_Performance.Models.Workout myworkout = workoutsViewModel.createWorkout();
                int wid = myworkout.ID;
                return Json(new { newUrl = Url.Action("WorkoutCreated", "Workouts", new { id = wid }) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult WorkoutCreated(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workout workout = db.Workouts.Find(id);
            if (workout == null)
            {
                return HttpNotFound();
            }
            Peak_Performance.Models.ViewModels.FullWorkoutViewModel fullworkout = new FullWorkoutViewModel(workout.ID);
            return View(fullworkout);
        }

        public async Task<int> ContactTeam(int team)
        {
            int newTeam = db.Teams.FirstOrDefault(t => t.ID == team).ID;
           List<Peak_Performance.Models.Athlete> athletes = db.Athletes.Where(a => a.Team.ID == newTeam).ToList();
            try
            {
                await SendGridMail("shariah.green1@gmail.com", "Shay Green");
                foreach (var athlete in athletes)
                {
                    AspNetUser user = db.AspNetUsers.FirstOrDefault(a => a.Id == athlete.Person.ASPNetIdentityID);
                    if (user != null)
                    {
                        if (user.Email != null)
                        {
                            string to = user.Email;
                            string name = athlete.Person.FirstName + " " + athlete.Person.LastName;
                            await SendGridMail(to, name);
                        }
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private async Task SendGridMail(string to, string name)
        {
            string apiKey = System.Web.Configuration.WebConfigurationManager.AppSettings["emailsenderkey"];
            var client = new SendGridClient(apiKey);
            var myMessage = new SendGridMessage();

            string img = System.IO.Path.GetFullPath(Server.MapPath("~\\Images\\Header.png"));
            SendGrid.Helpers.Mail.Attachment attachment = new SendGrid.Helpers.Mail.Attachment();
            attachment.Filename = img;
            string contentid = "Header";
            attachment.ContentId = contentid;

            myMessage.AddTo(to);
            myMessage.SetFrom(new EmailAddress("peakperformancewou@gmail.com", "Peak Performance"));
            myMessage.SetSubject("New Peak Performance Workout Available");
            //myMessage.AddContent(MimeType.Text, message.Body);
            myMessage.AddContent(MimeType.Html, "<hmtl><head/><body><div><img src=\"cid:" + contentid + "\"></div><div><h2> Hello " + name + ",</h2><h3> You have a new workout available for view at www.peakperformancedev.azurewebsites.net </h3></div></body></html>");
            var response = await client.SendEmailAsync(myMessage);
        }


        public void notify(string to, string name)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("peakperformancewou@gmail.com");
                mail.To.Add(to);
                mail.Subject = "New Peak Performance Workout Available";
                mail.IsBodyHtml = true;

                string img = System.IO.Path.GetFullPath(Server.MapPath("~\\Images\\Header.png"));
                System.Net.Mail.Attachment picAttachment = new System.Net.Mail.Attachment(img);
                string contentid = "Header";
                picAttachment.ContentId = contentid;
                mail.Attachments.Add(picAttachment);

                mail.Body = "<hmtl><head/><body><div><img src=\"cid:" + contentid + "\"></div><div><h2> Hello " + name + ",</h2><h3> You have a new workout available for view at www.peakperformancedev.azurewebsites.net </h3></div></body></html>";

                string username = "peakperformancewou@gmail.com";
                string pwd = System.Web.Configuration.WebConfigurationManager.AppSettings["PeakPerformanceEmail"];

                SmtpServer.Port = 587;
                SmtpServer.EnableSsl = true;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(username, pwd);

                SmtpServer.Send(mail);
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult SearchByText(string text)
        {
            IEnumerable<string> result = db.Exercises.Where(e => e.Name.Contains(text)).Select(e => e.Name).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchByMuscle(string MuscleGroupsId)
        {
            IEnumerable<string> result = db.ExcerciseMuscleGroups.Where(p => p.MuscleGroup.Name.Contains(MuscleGroupsId)).Select(p => p.Exercis.Name).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        // GET: Coach/Workouts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workout workout = db.Workouts.Find(id);
            if (workout == null)
            {
                return HttpNotFound();
            }
            return View(workout);
        }

        // GET: Coach/Workouts/Create
        public ActionResult Create()
        {
            string id = User.Identity.GetUserId();
            //Peak_Performance.Models.Coach temp = db.Coaches.FirstOrDefault(p => p.UserId == id);
            //ViewBag.TeamID = new SelectList(db.Teams.Where(t => t.Coach == temp), "TeamId", "TeamName");
            return View();
        }

        // POST: Coach/Workouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkoutsId,WorkoutDate,TeamID")] Workout workout)
        {
            if (ModelState.IsValid)
            {
                db.Workouts.Add(workout);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeamID = new SelectList(db.Teams, "TeamId", "TeamName", workout.TeamID);
            return View(workout);
        }

        // GET: Coach/Workouts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workout workout = db.Workouts.Find(id);
            if (workout == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeamID = new SelectList(db.Teams, "TeamId", "TeamName", workout.TeamID);
            return View(workout);
        }

        // POST: Coach/Workouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkoutsId,WorkoutDate,TeamID")] Workout workout)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workout).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TeamID = new SelectList(db.Teams, "TeamId", "TeamName", workout.TeamID);
            return View(workout);
        }

        // GET: Coach/Workouts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workout workout = db.Workouts.Find(id);
            if (workout == null)
            {
                return HttpNotFound();
            }
            return View(workout);
        }

        // POST: Coach/Workouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Workout workout = db.Workouts.Find(id);
            db.Workouts.Remove(workout);
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


    public class RequireRouteValuesAttribute : ActionMethodSelectorAttribute {
        public RequireRouteValuesAttribute(string[] valueNames) {
            ValueNames = valueNames;
        }

        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo) {
            bool contains = false;
            foreach (var value in ValueNames) {
                contains = controllerContext.HttpContext.Request[value] != null;
                if (!contains) break;
            }
            return contains;
        }

        public string[] ValueNames { get; private set; }
    }
}
