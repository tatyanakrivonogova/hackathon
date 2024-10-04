using Xunit;
using Microsoft.Extensions.Options;
using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.Utils;
namespace HackathonProblem.Tests;

public class WishlistTest
{
    [Fact]
    public void GenerateWishlists_FillWishlistsWithRandomEmployeesOrder_WishlistsSizesAreEqualToEmployeesNumber()
    {
        // Arrange
        string juniorsFile = "Juniors5.csv";
        string teamLeadsFile = "Teamleads5.csv";
        var juniors = EmployeesReader.ReadJuniors(juniorsFile);
        var teamLeads = EmployeesReader.ReadTeamLeads(teamLeadsFile);

        // Act
        IEnumerable<Wishlist> juniorsWishlists = WishlistGenerator.GenerateWishlists(juniors, teamLeads);
        IEnumerable<Wishlist> teamleadsWishlists = WishlistGenerator.GenerateWishlists(juniors, teamLeads);
        
        // Assert
        Assert.All(juniorsWishlists, juniorsWishlist => Assert.Equal(teamLeads.Count(), juniorsWishlist.DesiredEmployees.Count()));
        Assert.All(teamleadsWishlists, teamleadsWishlist => Assert.Equal(juniors.Count(), teamleadsWishlist.DesiredEmployees.Count()));
    }

    [Fact]
    public void GenerateWishlists_FillWishlistsWithRandomEmployeesOrder_WishlistsContainAllEmployees()
    {
        // Arrange
        string juniorsFile = "Juniors5.csv";
        string teamLeadsFile = "Teamleads5.csv";
        var juniors = EmployeesReader.ReadJuniors(juniorsFile);
        var teamLeads = EmployeesReader.ReadTeamLeads(teamLeadsFile);
        HashSet<int> teamleadsIds = new HashSet<int>(teamLeads.Select(e => e.Id));
        HashSet<int> juniorsIds = new HashSet<int>(juniors.Select(e => e.Id));

        // Act
        IEnumerable<Wishlist> juniorsWishlists = WishlistGenerator.GenerateWishlists(juniors, teamLeads);
        IEnumerable<Wishlist> teamleadsWishlists = WishlistGenerator.GenerateWishlists(juniors, teamLeads);
        
        // Assert
        Assert.All(juniorsWishlists, juniorsWishlist => Assert.True(teamleadsIds.SetEquals(
                                                        new HashSet<int>(juniorsWishlist.DesiredEmployees))));
        Assert.All(teamleadsWishlists, teamleadsWishlist => Assert.True(juniorsIds.SetEquals(
                                                        new HashSet<int>(teamleadsWishlist.DesiredEmployees))));
    }
}