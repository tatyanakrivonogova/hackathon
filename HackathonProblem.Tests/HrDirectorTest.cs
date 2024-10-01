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
    public void HRdirectorCountAverageHarmonicOfEqual()
    {
        // Arrange
        double tolerance = 0.00001;
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
    public void HRdirectorCountAverageHarmonic()
    {
        // Arrange
        List<int> indexes = new List<int>();
        indexes.Add(2);
        indexes.Add(6);
        HRDirector director = new HRDirector();

        // Act
        double averageHarmonic = director.CountAverageHarmonic(indexes);

        // Assert
        Assert.Equal(Convert.ToDouble(3), averageHarmonic, 14);
    }

    [Fact]
    public void HRdirectorCountScoreTest()
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
        var teams = manager.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);
        HRDirector director = new HRDirector();

        // Act
        double score = director.CountScore(teamLeads, juniors, teams, teamLeadsWishlists, juniorsWishlists);

        // Assert
        Assert.Equal(60.0/23, score);
    }
}