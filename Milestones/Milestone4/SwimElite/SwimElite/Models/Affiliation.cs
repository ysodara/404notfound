namespace SwimElite.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Affiliation")]
    public partial class Affiliation
    {
        public int ID { get; set; }

        public int? PersonID { get; set; }

        public int? TeamID { get; set; }

        public virtual Person Person { get; set; }

        public virtual Team Team { get; set; }
    }
}