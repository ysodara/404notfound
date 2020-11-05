using Peak_Performance.Models;
namespace Peak_Performance.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PeakPerformanceContext : DbContext
    {
        public PeakPerformanceContext()
            : base("name=PeakPerformanceContext")
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Athlete> Athletes { get; set; }
        public virtual DbSet<Coach> Coaches { get; set; }
        public virtual DbSet<Complex> Complexes { get; set; }
        public virtual DbSet<ComplexItem> ComplexItems { get; set; }
        public virtual DbSet<ExcerciseMuscleGroup> ExcerciseMuscleGroups { get; set; }
        public virtual DbSet<ExerciseRecord> ExerciseRecords { get; set; }
        public virtual DbSet<Exercis> Exercises { get; set; }
        public virtual DbSet<MuscleGroup> MuscleGroups { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Record> Records { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Workout> Workouts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Athlete>()
                .Property(e => e.Sex)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Exercis>()
                .HasMany(e => e.ComplexItems)
                .WithRequired(e => e.Exercis)
                .HasForeignKey(e => e.ExerciseID);

            modelBuilder.Entity<Exercis>()
                .HasMany(e => e.ExcerciseMuscleGroups)
                .WithRequired(e => e.Exercis)
                .HasForeignKey(e => e.ExerciseID);

            modelBuilder.Entity<Exercis>()
                .HasMany(e => e.ExerciseRecords)
                .WithRequired(e => e.Exercis)
                .HasForeignKey(e => e.ExerciseID);

            modelBuilder.Entity<Person>()
                .HasOptional(e => e.Athlete)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Person>()
                .HasOptional(e => e.Coach)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Workout>()
                .HasMany(e => e.Complexes)
                .WithOptional(e => e.Workout)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Workout>()
                .HasMany(e => e.Records)
                .WithRequired(e => e.Workout)
                .WillCascadeOnDelete(false);
        }
    }
}
