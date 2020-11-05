namespace Peak_Performance.Models.ViewModels {
    public class PersonsViewModel {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PreferredName { get; set; }
        public bool Active { get; set; }
        public string ASPNetIdentityID { get; set; }
    }
}