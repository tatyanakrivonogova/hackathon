using Xunit;
using Microsoft.Extensions.Options;
using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.Utils;
namespace HackathonProblem.Tests;

public class WishlistTest
{
    [Fact]
    public void WishlistSizeTest()
    {
        string juniorsFile = "Juniors5.csv";
        string teamLeadsFile = "Teamleads5.csv";
        var juniors = EmployeesReader.ReadJuniors(juniorsFile);
        var teamLeads = EmployeesReader.ReadTeamLeads(teamLeadsFile);
        IEnumerable<Wishlist> juniorsWishlists = WishlistGenerator.GenerateWishlists(juniors, teamLeads);
        IEnumerable<Wishlist> teamleadsWishlists = WishlistGenerator.GenerateWishlists(juniors, teamLeads);
        
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
    public void WishlistContentTest()
    {
        string juniorsFile = "Juniors5.csv";
        string teamLeadsFile = "Teamleads5.csv";
        var juniors = EmployeesReader.ReadJuniors(juniorsFile);
        var teamLeads = EmployeesReader.ReadTeamLeads(teamLeadsFile);
        IEnumerable<Wishlist> juniorsWishlists = WishlistGenerator.GenerateWishlists(juniors, teamLeads);
        IEnumerable<Wishlist> teamleadsWishlists = WishlistGenerator.GenerateWishlists(juniors, teamLeads);
        
        foreach (Employee junior in juniors)
        {
            var wishlist = juniorsWishlists.FirstOrDefault(w => w.EmployeeId == junior.Id);
            Assert.NotNull(wishlist);
            foreach (Employee teamLead in teamLeads)
            {
                Assert.Contains(teamLead.Id, wishlist.DesiredEmployees);
            }
        }
        foreach (Employee teamLead in teamLeads)
        {
            var wishlist = teamleadsWishlists.FirstOrDefault(w => w.EmployeeId == teamLead.Id);
            Assert.NotNull(wishlist);
            foreach (Employee junior in juniors)
            {
                Assert.Contains(junior.Id, wishlist.DesiredEmployees);
            }
        }
    }
}