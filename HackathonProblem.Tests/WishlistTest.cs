using Xunit;
using Microsoft.Extensions.Options;
using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.Utils;
namespace HackathonProblem.Tests;

public class WishlistTest
{
    [Fact]
    public void GenerateWishlists_FillWishlistsWithRandomEmployeesOrder_WishlistsSizesIsEqualToEmployeesNumber()
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
        foreach (Employee junior in juniors)
        {
            var wishlist = juniorsWishlists.FirstOrDefault(w => w.EmployeeId == junior.Id);
            Assert.NotNull(wishlist);
            Assert.Equal(teamLeads.Count(), wishlist.DesiredEmployees.Count());
        }
        foreach (Employee teamLead in teamLeads)
        {
            var wishlist = teamleadsWishlists.FirstOrDefault(w => w.EmployeeId == teamLead.Id);
            Assert.NotNull(wishlist);
            Assert.Equal(juniors.Count(), wishlist.DesiredEmployees.Count());
        }
    }

    [Fact]
    public void GenerateWishlists_FillWishlistsWithRandomEmployeesOrder_WishlistsContainAllEmployees()
    {
        // Arrange
        string juniorsFile = "Juniors5.csv";
        string teamLeadsFile = "Teamleads5.csv";
        var juniors = EmployeesReader.ReadJuniors(juniorsFile);
        var teamLeads = EmployeesReader.ReadTeamLeads(teamLeadsFile);

        // Act
        IEnumerable<Wishlist> juniorsWishlists = WishlistGenerator.GenerateWishlists(juniors, teamLeads);
        IEnumerable<Wishlist> teamleadsWishlists = WishlistGenerator.GenerateWishlists(juniors, teamLeads);

        HashSet<int> teamleadsIds = new HashSet<int>(teamLeads.Select(e => e.Id));
        HashSet<int> juniorsIds = new HashSet<int>(juniors.Select(e => e.Id));
        IEnumerable<int> commonTeamleadsIds = teamleadsIds;
        IEnumerable<int> commonJuniorsIds = juniorsIds;
        
        // get intersect of all juniors from teamleads' wishlists
        foreach (Wishlist wishlist in teamleadsWishlists)
        {
            HashSet<int> juniorsFromWishlist = new HashSet<int>(wishlist.DesiredEmployees);
            commonJuniorsIds = commonJuniorsIds.Intersect(juniorsFromWishlist);
        }
        // get intersect of all teamleads from juniors' wishlists
        foreach (Wishlist wishlist in juniorsWishlists)
        {
            HashSet<int> teamleadsFromWishlist = new HashSet<int>(wishlist.DesiredEmployees);
            commonTeamleadsIds = commonTeamleadsIds.Intersect(teamleadsFromWishlist);
        }
        
        // Assert
        Assert.True(commonTeamleadsIds.SequenceEqual(teamleadsIds));
        Assert.True(commonJuniorsIds.SequenceEqual(juniorsIds));
    }
}