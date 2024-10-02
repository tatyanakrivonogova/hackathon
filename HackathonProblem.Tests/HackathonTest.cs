using Xunit;
using Moq;
using Microsoft.Extensions.Options;
using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.Strategies;
using Nsu.HackathonProblem.HR;
using Nsu.HackathonProblem.Utils;
namespace HackathonProblem.Tests;

public class HackathonTest
{
    [Fact]
    public void HackathonScoreTest()
    {
        // Arrange
        string juniorsFile = "Juniors5.csv";
        string teamLeadsFile = "Teamleads5.csv";
        var juniors = EmployeesReader.ReadJuniors(juniorsFile);
        var teamLeads = EmployeesReader.ReadTeamLeads(teamLeadsFile);

        IEnumerable<Wishlist> juniorsWishlists = TestDataInitializer.GetJuniorsWishlist();
        IEnumerable<Wishlist> teamLeadsWishlists = TestDataInitializer.GetTeamLeadsWishlist();

        HRManager manager = new HRManager(new BaseTeamBuildingStrategy());
        HRDirector director = new HRDirector();
        Hackathon hackathon = new Hackathon();

        // Act
        double score = hackathon.RunHackathon(manager, director, teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);

        // Assert
        Assert.Equal(60.0/23, score);
    }
}