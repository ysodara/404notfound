namespace Peak_Performance.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sport
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string SportName { get; set; }

        [Required]
        [StringLength(100)]
        public string Season { get; set; }
    }
}
