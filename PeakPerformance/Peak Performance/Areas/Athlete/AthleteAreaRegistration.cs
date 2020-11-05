using System.Web.Mvc;

namespace Peak_Performance.Areas.Athlete {
    public class AthleteAreaRegistration : AreaRegistration {
        public override string AreaName {
            get {
                return "Athlete";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) {
            context.MapRoute("Athlete_default", "Athlete/{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}