namespace Peak_Performance.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ComplexItem
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string ExerciseName { get; set; }

        public int? ComplexReps { get; set; }

        public int? ComplexSets { get; set; }

        public double? LiftWeight { get; set; }

        public double? RunSpeed { get; set; }

        public TimeSpan? RunTime { get; set; }

        public double? RunDistance { get; set; }

        public int? ExerciseID { get; set; }

        public int ComplexId { get; set; }

        public virtual Complex Complex { get; set; }

        public virtual Exercis Exercis { get; set; }
    }
}
