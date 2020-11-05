namespace SwimElite.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Login")]
    public partial class Login
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Login()
        {
            People = new HashSet<Person>();
        }

        public int ID { get; set; }

        [StringLength(200)]
        public string UserName { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        [MaxLength(64)]
        public byte[] PasswordHash { get; set; }

        public bool? Active { get; set; }

        public int? RoleID { get; set; }

        public virtual Role Role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Person> People { get; set; }
    }
}