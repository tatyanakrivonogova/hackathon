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
    [Theory]
    [InlineData(10, 5)]
    [InlineData(1, 5)]
    [InlineData(100, 7)]
    public void CountHarmonicMean_CountHarmonicMeanOfTheSameNumber_HarmonicMeanIsEqualToNumbers(int n, int index)
    {
        // Arrange
        List<int> indexes = new List<int>();
        for (int i = 0; i < n; ++i)
        {
            indexes.Add(index);
        }
        HRDirector director = new HRDirector();

        // Act
        double harmonicMean = HarmonicMeanCounter.CountHarmonicMean(indexes);

        // Assert
        Assert.Equal(Convert.ToDouble(index), harmonicMean, 13);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    public void CountHarmonicMean_CountHarmonicMeanOfZeros_ReturnOne(int n)
    {
        // Arrange
        List<int> indexes = new List<int>();
        for (int i = 0; i < n; ++i)
        {
            indexes.Add(0);
        }
        HRDirector director = new HRDirector();

        // Act
        Action throwingAction = () => { HarmonicMeanCounter.CountHarmonicMean(indexes); };
    
        // Assert
        Assert.Throws<ArgumentException>(throwingAction);
    }

    [Theory]
    [InlineData(2, 6, 3.0)]
    [InlineData(2, 8, 3.2)]
    [InlineData(20, 180, 36)]
    public void CountHarmonicMean_CountHarmonicMeanOfTwoFixNumbers_ResultIsEqualToHarmonicMean(int a, int b, double result)
    {
        // Arrange
        List<int> indexes = new List<int>() {a, b};
        HRDirector director = new HRDirector();

        // Act
        double harmonicMean = HarmonicMeanCounter.CountHarmonicMean(indexes);

        // Assert
        Assert.Equal(result, harmonicMean, 14);
    }

    [Fact]
    public void CountScore_CountHarmonicMeanForBuiltTeams_ResultIsEqualToHarmonicMean()
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