using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BrodihyHabitTracker.Models;

namespace BrodihyHabitTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Habit> Habits { get; set; }
        public DbSet<HabitCompletion> HabitCompletions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure HabitCompletion to prevent duplicate entries for same habit/day
            builder.Entity<HabitCompletion>()
                .HasIndex(hc => new { hc.HabitId, hc.CompletionDate, hc.UserId })
                .IsUnique();

            // Configure Habit relationships
            builder.Entity<Habit>()
                .HasOne(h => h.User)
                .WithMany(u => u.Habits)
                .HasForeignKey(h => h.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed habit types if needed
            // Add any additional configuration here
        }
    }
}