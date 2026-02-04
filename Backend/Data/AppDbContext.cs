using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Habit> Habits { get; set; } = null!;
    public DbSet<Completion> Completions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Completion>()
            .HasOne<Habit>()
            .WithMany(h => h.Completions)
            .HasForeignKey(c => c.HabitId);

        modelBuilder.Entity<Completion>()
            .HasIndex(c => new { c.HabitId, c.CompletedDate })
            .IsUnique();
    }
}
