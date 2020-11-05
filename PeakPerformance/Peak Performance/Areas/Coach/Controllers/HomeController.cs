namespace Peak_Performance.Areas.Coach.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Peak_Performance.DAL;
    using Peak_Performance.Models;

    [Authorize(Roles = "Coach")]
    public class HomeController : Controller
    {
        private PeakPerformanceContext db = new PeakPerformanceContext();

        public ActionResult Index()
        {
            string id = User.Identity.GetUserId();
            Person temp = db.Persons.FirstOrDefault(p => p.ASPNetIdentityID == id);
            CoachProfileViewModel coach = new CoachProfileViewModel(temp.ID);
            return View("Index", coach);
        }

        [HttpPost]
        [Authorize]
        public ActionResult UploadPhoto(HttpPostedFileBase postedFile)
        {
            byte[] bytes;
            using (BinaryReader br = new BinaryReader(postedFile.InputStream))
            {
                bytes = br.ReadBytes(postedFile.ContentLength);
            }
            string ID = User.Identity.GetUserId();
            Person person = db.Persons.Where(r => r.ASPNetIdentityID == ID).FirstOrDefault();
            person.ProfilePic = bytes;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /AddAthlete
        public ActionResult AddAthlete()
        {
            string id = User.Identity.GetUserId();
            Person temp = db.Persons.FirstOrDefault(p => p.ASPNetIdentityID == id);

            CoachProfileViewModel coach = new CoachProfileViewModel(temp.ID);

            return View("AddAthlete", coach);
        }

        // POST: /AddAthlete
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult AddAthlete(CoachProfileViewModel vm)
        {
                Athlete a = db.Athletes.FirstOrDefault(x => x.ID == vm.athItem.ID);
                a.TeamID = vm.teamItem.ID;
                db.SaveChanges();
                return RedirectToAction("Index");
        }

        public ActionResult EditProfile()
        {
            string id = User.Identity.GetUserId();
            Person person = db.Persons.Where(p => p.ASPNetIdentityID == id).FirstOrDefault();
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile([Bind(Include = "ID,FirstName,LastName,PreferredName,Active,ASPNetIdentityID,ProfilePic")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }
    }
}