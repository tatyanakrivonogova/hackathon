using Xunit;
using Moq;
using Microsoft.Extensions.Options;
using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.Strategies;
using Nsu.HackathonProblem.HR;
using Nsu.HackathonProblem.Utils;
namespace HackathonProblem.Tests;

public class HRDirectorTest
{
    [Fact]
    public void CountAverageHarmonic_CountAverageHarmonicOfTheSameNumber_AverageHarmonicIsEqualToNumbers()
    {
        // Arrange
        List<int> indexes = new List<int>();
        int n = 10;
        int index = 5;
        for (int i = 0; i < n; ++i)
        {
            indexes.Add(index);
        }
        HRDirector director = new HRDirector();

        // Act
        double averageHarmonic = director.CountAverageHarmonic(indexes);

        // Assert
        Assert.Equal(Convert.ToDouble(index), averageHarmonic, 14);
    }

    [Fact]
    public void CountAverageHarmonic_CountAverageHarmonicOfTwoFixNumbers_ResultIsEqualToAverageHarmonic()
    {
        // Arrange
        List<int> indexes = new List<int>();
        indexes.Add(2);
        indexes.Add(6);
        HRDirector director = new HRDirector();

        // Act
        double averageHarmonic = director.CountAverageHarmonic(indexes);

        // Assert
        Assert.Equal(3.0, averageHarmonic, 14);
    }

    [Fact]
    public void CountScore_CountAvergeHarmonicForBuiltTeams_ResultIsEqualToAverageHarmonic()
    {
        // Arrange
        string juniorsFile = "Juniors5.csv";
        string teamLeadsFile = "Teamleads5.csv";
        var juniors = EmployeesReader.ReadJuniors(juniorsFile);
        var teamLeads = EmployeesReader.ReadTeamLeads(teamLeadsFile);

        IEnumerable<Wishlist> juniorsWishlists = TestDataInitializer.GetJuniorsWishlist();
        IEnumerable<Wishlist> teamLeadsWishlists = TestDataInitializer.GetTeamLeadsWishlist();

        HRManager manager = new HRManager(new BaseTeamBuildingStrategy());
        var teams = manager.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);
        HRDirector director = new HRDirector();

        // Act
        double score = director.CountScore(teamLeads, juniors, teams, teamLeadsWishlists, juniorsWishlists);

        // Assert
        Assert.Equal(60.0 / 23, score, 14);
    }
}