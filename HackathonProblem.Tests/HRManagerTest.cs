using Xunit;
using Moq;
using Microsoft.Extensions.Options;
using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.Strategies;
using Nsu.HackathonProblem.HR;
using Nsu.HackathonProblem.Utils;
namespace HackathonProblem.Tests;

public class HRManagerTest
{
    [Fact]
    public void HRManagerTeamsCountTest()
    {
        // Arrange
        string juniorsFile = "Juniors5.csv";
        string teamLeadsFile = "Teamleads5.csv";
        var juniors = EmployeesReader.ReadJuniors(juniorsFile);
        var teamLeads = EmployeesReader.ReadTeamLeads(teamLeadsFile);
        IEnumerable<Wishlist> juniorsWishlists = WishlistGenerator.GenerateWishlists(juniors, teamLeads);
        IEnumerable<Wishlist> teamLeadsWishlists = WishlistGenerator.GenerateWishlists(juniors, teamLeads);
        HRManager manager = new HRManager(new BaseTeamBuildingStrategy());

        // Act
        var teams = manager.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);

        // Assert
        Assert.Equal(juniors.Count(), teams.Count());
        Assert.Equal(teamLeads.Count(), teams.Count());
    }

    [Fact]
    public void HRManagerTeamsContentTest()
    {
        // Arrange
        string juniorsFile = "Juniors5.csv";
        string teamLeadsFile = "Teamleads5.csv";
        var juniors = EmployeesReader.ReadJuniors(juniorsFile);
        var teamLeads = EmployeesReader.ReadTeamLeads(teamLeadsFile);

        List<Wishlist> juniorsDesiredEmployees = new List<Wishlist>();
        juniorsDesiredEmployees.Add(new Wishlist(1, new int[] { 1, 2, 3, 4, 5 }));
        juniorsDesiredEmployees.Add(new Wishlist(2, new int[] { 1, 3, 5, 4, 2 }));
        juniorsDesiredEmployees.Add(new Wishlist(3, new int[] { 4, 5, 2, 1, 3 }));
        juniorsDesiredEmployees.Add(new Wishlist(4, new int[] { 2, 3, 1, 4, 5 }));
        juniorsDesiredEmployees.Add(new Wishlist(5, new int[] { 1, 2, 4, 5, 3 }));
        IEnumerable<Wishlist> juniorsWishlists = juniorsDesiredEmployees;

        List<Wishlist> teamLeadsDesiredEmployees = new List<Wishlist>();
        teamLeadsDesiredEmployees.Add(new Wishlist(1, new int[] { 2, 1, 4, 3, 5 }));
        teamLeadsDesiredEmployees.Add(new Wishlist(2, new int[] { 4, 3, 5, 1, 2 }));
        teamLeadsDesiredEmployees.Add(new Wishlist(3, new int[] { 4, 2, 5, 1, 3 }));
        teamLeadsDesiredEmployees.Add(new Wishlist(4, new int[] { 2, 3, 1, 5, 4 }));
        teamLeadsDesiredEmployees.Add(new Wishlist(5, new int[] { 4, 1, 2, 5, 3 }));
        IEnumerable<Wishlist> teamLeadsWishlists = teamLeadsDesiredEmployees;

        HRManager manager = new HRManager(new BaseTeamBuildingStrategy());

        // Act
        var teams = manager.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);

        // Assert
        List<Team> teamsArray = teams.ToList();
        Assert.Equal(1, teamsArray[0].TeamLead.Id);
        Assert.Equal(2, teamsArray[0].Junior.Id);
        Assert.Equal(2, teamsArray[1].TeamLead.Id);
        Assert.Equal(4, teamsArray[1].Junior.Id);
        Assert.Equal(3, teamsArray[2].TeamLead.Id);
        Assert.Equal(5, teamsArray[2].Junior.Id);
        Assert.Equal(4, teamsArray[3].TeamLead.Id);
        Assert.Equal(3, teamsArray[3].Junior.Id);
        Assert.Equal(5, teamsArray[4].TeamLead.Id);
        Assert.Equal(1, teamsArray[4].Junior.Id);
    }

    [Fact]
    public void HRManagerCallStrategyOneTimeTest()
    {
        // Arrange
        string juniorsFile = "Juniors5.csv";
        string teamLeadsFile = "Teamleads5.csv";
        var juniors = EmployeesReader.ReadJuniors(juniorsFile);
        var teamLeads = EmployeesReader.ReadTeamLeads(teamLeadsFile);
        IEnumerable<Wishlist> juniorsWishlists = WishlistGenerator.GenerateWishlists(juniors, teamLeads);
        IEnumerable<Wishlist> teamLeadsWishlists = WishlistGenerator.GenerateWishlists(juniors, teamLeads);
        var mock = new Mock<ITeamBuildingStrategy>();
        HRManager manager = new HRManager(mock.Object);

        // Act
        var teams = manager.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);

        // Assert
        mock.Verify(x => x.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists), Times.Once());
    }

}