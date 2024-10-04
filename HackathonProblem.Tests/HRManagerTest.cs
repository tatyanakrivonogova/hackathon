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
    public void BuildTeams_BuildTeamsDueToSpecifiedWishlists_TeamsCountIsEqualToEmployeesCount()
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
    public void BuildTeams_BuildTeamsDueToSpecifiedWishlists_TeamsContainPairsDueToAlgorithm()
    {
        // Arrange
        string juniorsFile = "Juniors5.csv";
        string teamLeadsFile = "Teamleads5.csv";
        var juniors = EmployeesReader.ReadJuniors(juniorsFile);
        var teamLeads = EmployeesReader.ReadTeamLeads(teamLeadsFile);

        IEnumerable<Wishlist> juniorsWishlists = TestDataInitializer.GetJuniorsWishlist();
        IEnumerable<Wishlist> teamLeadsWishlists = TestDataInitializer.GetTeamLeadsWishlist();

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
    public void BuildTeams_BuildTeamsDueToSpecifiedWishlists_MethodIsCalledOnce()
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

    [Fact]
    public void BuildTeams_BuildTeamsOfDifferentNumbersOfEmployees_ThtowsArgumentException()
    {
        // Arrange
        string juniorsFile = "Juniors5.csv";
        string teamLeadsFile = "Teamleads20.csv";
        var juniors = EmployeesReader.ReadJuniors(juniorsFile);
        var teamLeads = EmployeesReader.ReadTeamLeads(teamLeadsFile);
        IEnumerable<Wishlist> juniorsWishlists = WishlistGenerator.GenerateWishlists(juniors, teamLeads);
        IEnumerable<Wishlist> teamLeadsWishlists = WishlistGenerator.GenerateWishlists(juniors, teamLeads);
        HRManager manager = new HRManager(new BaseTeamBuildingStrategy());
        Action throwingAction = () => { manager.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists); };
    
        // Assert
        Assert.Throws<ArgumentException>(throwingAction);
    }

}