using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.Dto;

public class HackathonContext : DbContext
{
    string configuration;
    public DbSet<HackathonDto> Hackathon { get; init; }
    public DbSet<EmployeeDto> Employee { get; init; }
    public DbSet<WishlistDto> Wishlist { get; init; }
    public DbSet<TeamDto> Team { get; init; }

    public HackathonContext(string database)
    {
        configuration = database;
        // Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration);
                //   .LogTo(Console.WriteLine, LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WishlistDto>()
            .HasOne(w => w.Employee)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TeamDto>()
            .HasOne(t => t.Junior)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TeamDto>()
            .HasOne(t => t.TeamLead)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
