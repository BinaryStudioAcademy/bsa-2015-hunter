namespace Hunter.DataAccess.Db
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HunterDbContext : DbContext
    {
        public HunterDbContext()
            : base("name=HunterDb")
        {
            Database.SetInitializer<HunterDbContext>(new CreateDatabaseIfNotExists<HunterDbContext>());
        }

        public virtual DbSet<Activity> Activity { get; set; }
        public virtual DbSet<Candidate> Candidate { get; set; }
        public virtual DbSet<Card> Card { get; set; }
        public virtual DbSet<Feedback> Feedback { get; set; }
        public virtual DbSet<Interview> Interview { get; set; }
        public virtual DbSet<Pool> Pool { get; set; }
        public virtual DbSet<Resume> Resume { get; set; }
        public virtual DbSet<SpecialNote> SpecialNote { get; set; }
        public virtual DbSet<Test> Test { get; set; }
        public virtual DbSet<UserProfile> UserProfile { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Vacancy> Vacancy { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>()
                .HasMany(e => e.Card)
                .WithRequired(e => e.Candidate)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Candidate>()
                .HasMany(e => e.Pool)
                .WithMany(e => e.Candidate)
                .Map(m => m.ToTable("Candidate_Pool").MapLeftKey("CandidateId").MapRightKey("PoolId"));

            modelBuilder.Entity<Card>()
                .HasMany(e => e.Feedback)
                .WithRequired(e => e.Card)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Card>()
                .HasMany(e => e.Interview)
                .WithRequired(e => e.Card)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Card>()
                .HasMany(e => e.SpecialNote)
                .WithRequired(e => e.Card)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Card>()
                .HasMany(e => e.Test)
                .WithRequired(e => e.Card)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Interview>()
                .Property(e => e.Comments)
                .IsFixedLength();

            modelBuilder.Entity<Interview>()
                .Property(e => e.FeedbackId)
                .IsFixedLength();

            modelBuilder.Entity<Pool>()
                .HasMany(e => e.Vacancy)
                .WithRequired(e => e.Pool)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Resume>()
                .HasMany(e => e.Candidate)
                .WithRequired(e => e.Resume)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserProfile>()
                .HasMany(e => e.Candidate)
                .WithOptional(e => e.UserProfile)
                .HasForeignKey(e => e.AddedByProfileId);

            modelBuilder.Entity<UserProfile>()
                .HasMany(e => e.Card)
                .WithOptional(e => e.UserProfile)
                .HasForeignKey(e => e.AddedByProfileId);

            modelBuilder.Entity<UserProfile>()
                .HasMany(e => e.Feedback)
                .WithOptional(e => e.UserProfile)
                .HasForeignKey(e => e.ProfileId);

            modelBuilder.Entity<UserRole>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.UserRole)
                .HasForeignKey(e => e.RoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vacancy>()
                .HasMany(e => e.Card)
                .WithRequired(e => e.Vacancy)
                .WillCascadeOnDelete(false);
        }
    }
}