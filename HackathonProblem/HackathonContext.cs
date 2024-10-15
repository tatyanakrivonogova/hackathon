using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.Dto;

public class HackathonContext : DbContext
{
    string configuration;
    public DbSet<HackathonDto> Hackathon { get; init; }
    public DbSet<EmployeeDto> Employee { get; init; }
    public DbSet<ParticipantDto> Participant { get; init; }
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
        // modelBuilder.Entity<EmployeeDto>()
        //     .HasKey(e => new { e.Id, e.Name, e.Role, e.HackathonId });

        modelBuilder.Entity<ParticipantDto>()
            .HasKey(p => new { p.EmployeePk, p.HackathonId });
        
        // modelBuilder.Entity<WishlistDto>()
        //     .HasKey(w => new { w.EmployeeId, w.DesiredEmployees, w.HackathonId });

        modelBuilder.Entity<TeamDto>()
            .HasKey(t => new { t.TeamLeadId, t.JuniorId, t.HackathonId });

        // modelBuilder.Entity<EmployeeDto>()
        //     .HasOne(e => e.Hackathon)
        //     .WithMany(h => h.Employees)
        //     .HasForeignKey(e => e.HackathonId)
        //     .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ParticipantDto>()
            .HasOne(p => p.Employee)
            .WithMany()
            .HasForeignKey(p => p.EmployeePk)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ParticipantDto>()
            .HasOne(p => p.Hackathon)
            .WithMany(h => h.Participants)
            .HasForeignKey(p => p.HackathonId)
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
