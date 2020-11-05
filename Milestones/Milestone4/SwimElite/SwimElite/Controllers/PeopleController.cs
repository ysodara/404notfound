using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SwimElite.Models;

namespace SwimElite.Controllers
{
    public class PeopleController : Controller
    {
        private SwimEliteContext db = new SwimEliteContext();

        // GET: People
        public ActionResult Index()
        {
            var people = db.People.Include(p => p.Login);
            return View(people.ToList());
        }

        public ActionResult Search(string search)
        {
            IEnumerable<SwimElite.Models.Person> results = db.People.Where(p => p.FirstName.Contains(search)).ToList();
            return View(results);
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            IEnumerable<SwimElite.Models.Time> times = db.Times.Include(t => t.Event).Include(t => t.Person).Where(p => p.Person.ID == person.ID).ToList();
            List<SwimElite.Models.Time> prtimes = new List<SwimElite.Models.Time>();
            foreach(var swimevent in db.Events)
            {
                SwimElite.Models.Time PR = new SwimElite.Models.Time();
                PR.Time1 = System.TimeSpan.MaxValue;
                var check = false;
                foreach(var time in times)
                {
                    if (time.Time1 != null && time.Time1 < PR.Time1 && time.Event.ID == swimevent.ID)
                    {
                        PR = time;
                        check = true;
                    }
                }
                if (check == true)
                {
                    prtimes.Add(PR);
                }
                
            }
            ViewBag.times = times;
            ViewBag.prtimes = prtimes;
            return View(person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            ViewBag.LoginID = new SelectList(db.Logins, "ID", "UserName");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Gender,Age,Active,LoginID")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoginID = new SelectList(db.Logins, "ID", "UserName", person.LoginID);
            return View(person);
        }

        // GET: People/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoginID = new SelectList(db.Logins, "ID", "UserName", person.LoginID);
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Gender,Age,Active,LoginID")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoginID = new SelectList(db.Logins, "ID", "UserName", person.LoginID);
            return View(person);
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.People.Find(id);
            db.People.Remove(person);
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