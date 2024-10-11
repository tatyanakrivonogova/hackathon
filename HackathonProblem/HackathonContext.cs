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
}
