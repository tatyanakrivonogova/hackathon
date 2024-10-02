using Nsu.HackathonProblem.Utils;
using Nsu.HackathonProblem.HR;

namespace Nsu.HackathonProblem.Contracts
{
    public class Hackathon
    {
        public double RunHackathon(HRManager manager, HRDirector director, 
                IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors,
                IEnumerable<Wishlist> teamLeadsWishlists = null,
                IEnumerable<Wishlist> juniorsWishlists = null)
        {
            // generate wishlists randomly
            if (juniorsWishlists == null) juniorsWishlists = WishlistGenerator.GenerateWishlists(juniors, teamLeads);
            if (teamLeadsWishlists == null) teamLeadsWishlists = WishlistGenerator.GenerateWishlists(teamLeads, juniors);

            // creating teams by wishlists
            var teams = manager.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);
            return director.CountScore(teamLeads, juniors, teams, teamLeadsWishlists, juniorsWishlists);
        }
    }
}