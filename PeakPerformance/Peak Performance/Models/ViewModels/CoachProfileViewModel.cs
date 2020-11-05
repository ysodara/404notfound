using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Peak_Performance.DAL;

namespace Peak_Performance.Models
{
    public class CoachProfileViewModel
    {
        private readonly PeakPerformanceContext db = new PeakPerformanceContext();

        public CoachProfileViewModel() {

        }

        public CoachProfileViewModel(int id)
        {
            coach = db.Coaches.Find(id);
            ProfilePic = coach.Person.ProfilePic;
            teams = db.Teams.Where(t => t.CoachID == id).ToList();
            teamList = new SelectList(db.Teams.Where(item => item.CoachID == coach.ID), "ID", "TeamName");
            CoachProfileId = coach.ID;
            athletes = new List<Athlete>();
            foreach (Team team in teams)
            {
                IEnumerable<Athlete> athletelist = db.Athletes.Where(a => a.TeamID == team.ID).ToList();
                if (athletelist != null)
                {
                    athletes.AddRange(athletelist);
                }
            }

            var fullAthName = db.Athletes.Where(m => m.TeamID == null).Select(x => new { id = x.ID, name = x.Person.FirstName + " " + x.Person.LastName });

            athList = new SelectList(fullAthName, "id", "name");
            //athList = new SelectList(db.Athletes.Where(r => r.Person.ASPNetIdentityID == db.AspNetUsers.Select(b => b.AspNetRoles.Where(c => c.Id == "3").Select(e => e.Id).FirstOrDefault()).FirstOrDefault()), "ID", "Email");
            //athList = new SelectList(db.AspNetRoles.Where(r => r.Id == "3").Select(t => t.AspNetUsers), "Id", "Email");
        }

        public int CoachProfileId { get; set; }
        public virtual IEnumerable<Team> teams { get; set; }
        public virtual Coach coach { get; set; }
        public virtual List<Athlete> athletes { get; set; }
        public IEnumerable<SelectListItem> teamList { get; set; }
        public Team teamItem { get; set; }
        public IEnumerable<SelectListItem> athList { get; set; }
        public Team athItem { get; set; }
        public virtual Byte[] ProfilePic { get; set; }

    }
}