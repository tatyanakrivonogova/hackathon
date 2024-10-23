using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Testcontainers.PostgreSql;
using Mapster;

using Nsu.HackathonProblem.DataTransfer;
using Nsu.HackathonProblem.Mapper;

public class DatabaseTest : IAsyncLifetime
{
    private PostgreSqlContainer _postgresContainer;
    IDataTransfer dataTransfer;
    public async Task InitializeAsync()
    {
        new RegisterMapper().Register(new TypeAdapterConfig());

        _postgresContainer = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .Build();

        await _postgresContainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _postgresContainer.StopAsync();
    }

    [Fact]
    public async Task SaveHackathon_SaveHackathonToDbAndLoadItById_GetEquivalentHackathon()
    {
        // Arrange
        var hackathon = TestDataInitializer.GetHackathon();
        var databaseOptions = new DatabaseOptions
        {
            database = _postgresContainer.GetConnectionString()
        };
        var options = Options.Create(databaseOptions);
        dataTransfer = new DatabaseDataTransfer(new HackathonContext(options));

        // Act
        dataTransfer.SaveHackathon(hackathon);
        var loadedHackathon = dataTransfer.LoadHackathonById(1);

        // Assert
        Assert.NotNull(loadedHackathon);
        Assert.Equal(hackathon.Score, loadedHackathon.Score);
        Assert.Equal(hackathon.Wishlists.Count(), loadedHackathon.Wishlists.Count());
        Assert.Equal(hackathon.Teams.Count(), loadedHackathon.Teams.Count());

        var wishlists = hackathon.Wishlists.ToList();
        var loadedWishlists = loadedHackathon.Wishlists.ToList();
        for (int i = 0; i < hackathon.Wishlists.Count(); ++i)
        {
            Assert.Equal(wishlists[i].Employee.Id, loadedWishlists[i].Employee.Id);
            Assert.Equal(wishlists[i].Employee.Name, loadedWishlists[i].Employee.Name);
            Assert.Equal(wishlists[i].DesiredEmployees, loadedWishlists[i].DesiredEmployees);
        }

        var teams = hackathon.Teams.ToList();
        var loadedTeams = loadedHackathon.Teams.ToList();
        for (int i = 0; i < hackathon.Teams.Count(); ++i)
        {
            Assert.Equal(teams[i].Junior.Id, loadedTeams[i].Junior.Id);
            Assert.Equal(teams[i].Junior.Id, loadedTeams[i].Junior.Id);
            Assert.Equal(teams[i].TeamLead.Name, loadedTeams[i].TeamLead.Name);
            Assert.Equal(teams[i].TeamLead.Name, loadedTeams[i].TeamLead.Name);
        }
    }
}
