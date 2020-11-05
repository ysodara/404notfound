namespace Peak_Performance.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Persons")]
    public partial class Person
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string PreferredName { get; set; }

        public bool? Active { get; set; }

        public Byte[] ProfilePic { get; set; }

        [Required]
        [StringLength(128)]
        public string ASPNetIdentityID { get; set; }

        public virtual Athlete Athlete { get; set; }

        public virtual Coach Coach { get; set; }
    }
}
