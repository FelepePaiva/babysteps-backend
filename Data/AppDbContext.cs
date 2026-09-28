using Microsoft.EntityFrameworkCore;
using BabySteps.API.Models;

namespace BabySteps.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Baby> Babies { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<Routine> Routines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Baby>(entity =>
            {
                entity.Property(b => b.BirthDate)
                      .HasColumnType("timestamp with time zone");

                entity.Property(b => b.CreatedAt)
                      .HasColumnType("timestamp with time zone");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.OccurredAt)
                      .HasColumnType("timestamp with time zone");

                entity.Property(e => e.CreatedAt)
                      .HasColumnType("timestamp with time zone");

                entity.Property(e => e.Type)
                      .HasConversion<string>();
            });

            modelBuilder.Entity<Assessment>(entity =>
            {
                entity.Property(a => a.CreatedAt)
                      .HasColumnType("timestamp with time zone");

                entity.Property(a => a.ExpectedBirthDate)
                      .HasColumnType("timestamp with time zone");
            });

            modelBuilder.Entity<Routine>(entity =>
            {
                entity.HasIndex(r => new { r.BabyId, r.Date })
                      .IsUnique();

                entity.Property(r => r.Date)
                      .HasColumnType("date");

                entity.Property(r => r.WakeUpTime)
                      .HasMaxLength(10);

                entity.Property(r => r.Plan)
                      .HasColumnType("jsonb");

                entity.Property(r => r.CreatedAt)
                      .HasColumnType("timestamp with time zone");

                entity.Property(r => r.UpdatedAt)
                      .HasColumnType("timestamp with time zone");
            });
        }
    }
}