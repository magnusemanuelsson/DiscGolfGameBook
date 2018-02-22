namespace WebApplication1
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<GameRound> GameRound { get; set; }
        public virtual DbSet<GolfCourse> GolfCourse { get; set; }
        public virtual DbSet<Hole> Hole { get; set; }
        public virtual DbSet<Player> Player { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasMany(e => e.GameRound)
                .WithRequired(e => e.Game1)
                .HasForeignKey(e => e.Game)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GolfCourse>()
                .HasMany(e => e.Game)
                .WithRequired(e => e.GolfCourse1)
                .HasForeignKey(e => e.GolfCourse)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GolfCourse>()
                .HasMany(e => e.Hole)
                .WithRequired(e => e.GolfCourse1)
                .HasForeignKey(e => e.GolfCourse)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hole>()
                .HasMany(e => e.GameRound)
                .WithRequired(e => e.Hole1)
                .HasForeignKey(e => e.Hole)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.Game)
                .WithRequired(e => e.Player1)
                .HasForeignKey(e => e.Player)
                .WillCascadeOnDelete(false);
        }
    }
}
