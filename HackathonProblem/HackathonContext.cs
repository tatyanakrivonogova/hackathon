using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.Dto;

public class HackathonContext : DbContext
{
    public DbSet<HackathonDto> Hackathon { get; init; }
    public DbSet<EmployeeDto> Employee { get; init; }
    public DbSet<WishlistDto> Wishlist { get; init; }
    public DbSet<TeamDto> Team { get; init; }

    public HackathonContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=hackathon;Username=postgres;Password=postgres");
                //   .LogTo(Console.WriteLine, LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeDto>()
            .HasOne(e => e.Hackathon)
            .WithMany(h => h.Employees)
            .HasForeignKey(e => e.HackathonId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WishlistDto>()
            .HasOne(w => w.Hackathon)
            .WithMany(h => h.Wishlists)
            .HasForeignKey(w => w.HackathonId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TeamDto>()
            .HasOne(t => t.Hackathon)
            .WithMany(h => h.Teams)
            .HasForeignKey(t => t.HackathonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
