namespace Peak_Performance.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Athlete
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Athlete()
        {
            ExerciseRecords = new HashSet<ExerciseRecord>();
            Records = new HashSet<Record>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(1)]
        public string Sex { get; set; }

        [StringLength(200)]
        public string Gender { get; set; }

        [Column(TypeName = "date")]
        public DateTime DOB { get; set; }

        public double? Height { get; set; }

        public double? Weight { get; set; }

        public int? TeamID { get; set; }

        [StringLength(50)]
        public string FitBitUserID { get; set; }

        public string FitBitAccessToken { get; set; }

        public virtual Person Person { get; set; }

        public virtual Team Team { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExerciseRecord> ExerciseRecords { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Record> Records { get; set; }
    }
}
