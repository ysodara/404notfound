namespace SwimElite.Models
{
    using System.Data.Entity;

    public partial class SwimEliteContext : DbContext
    {
        public SwimEliteContext()
            : base("name=SwimEliteContext")
        {
        }

        public virtual DbSet<Affiliation> Affiliations { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventList> EventLists { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Time> Times { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Login>()
                .Property(e => e.PasswordHash)
                .IsFixedLength();

            modelBuilder.Entity<Person>()
                .Property(e => e.Gender)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.Code)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}