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
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration);
                //   .LogTo(Console.WriteLine, LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TeamDto>()
            .HasKey(t => new { t.TeamLeadId, t.JuniorId, t.HackathonId });

        modelBuilder.Entity<WishlistDto>()
            .HasOne(w => w.Employee)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TeamDto>()
            .HasOne(t => t.Junior)
            .WithMany()
            .HasForeignKey(t => t.JuniorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TeamDto>()
            .HasOne(t => t.TeamLead)
            .WithMany()
            .HasForeignKey(t => t.TeamLeadId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
