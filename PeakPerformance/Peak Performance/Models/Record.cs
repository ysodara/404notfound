namespace Peak_Performance.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Record
    {
        public int ID { get; set; }

        public bool? Completed { get; set; }

        [StringLength(250)]
        public string Note { get; set; }

        public int AthleteID { get; set; }

        public int WorkoutID { get; set; }

        public virtual Athlete Athlete { get; set; }

        public virtual Workout Workout { get; set; }
    }
}
