namespace Peak_Performance.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ExerciseRecord
    {
        public int ExerciseRecordId { get; set; }

        public int? ComplexReps { get; set; }

        public int? ComplexSets { get; set; }

        public double? LiftWeight { get; set; }

        public double? RunSpeed { get; set; }

        public TimeSpan? RunTime { get; set; }

        public double? RunDistance { get; set; }

        public int ExerciseID { get; set; }

        public int AthleteID { get; set; }

        public virtual Athlete Athlete { get; set; }

        public virtual Exercis Exercis { get; set; }
    }
}
