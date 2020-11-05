using System.Web.Mvc;

namespace Peak_Performance.Areas.Coach {
    public class CoachAreaRegistration : AreaRegistration {
        public override string AreaName {
            get {
                return "Coach";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) {
            context.MapRoute("Coach_default",
                "Coach/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            context.MapRoute("Coach_AddAthlete",
                "Coach/AddAthlete/{id}",
                new { controller = "Home", action = "AddAthlete", id = UrlParameter.Optional });
        }
    }
}