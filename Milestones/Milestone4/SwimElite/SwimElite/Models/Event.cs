namespace SwimElite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Event")]
    public partial class Event
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Event()
        {
            Times = new HashSet<Time>();
        }

        public int ID { get; set; }

        [StringLength(200)]
        public string Location { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public int? EventListID { get; set; }

        public virtual EventList EventList { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Time> Times { get; set; }
    }
}