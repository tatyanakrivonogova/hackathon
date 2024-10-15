using System.ComponentModel.DataAnnotations;
using Nsu.HackathonProblem.Utils;
using Nsu.HackathonProblem.HR;

namespace Nsu.HackathonProblem.Contracts
{
    public class Hackathon
    {
        [Key] public int Id { get; set; }
        public double Score { get; set; }
        public IEnumerable<Employee>? TeamLeads { get; set; }
        public IEnumerable<Employee>? Juniors { get; set; }
        public IEnumerable<Wishlist>? Wishlists { get; set; }
        public IEnumerable<Team>? Teams { get; set; }
        public double RunHackathon(HRManager manager, HRDirector director,
                IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors,
                IEnumerable<Wishlist>? teamLeadsWishlists = null,
                IEnumerable<Wishlist>? juniorsWishlists = null)
        {
            // generate wishlists randomly
            if (juniorsWishlists == null) juniorsWishlists = WishlistGenerator.GenerateWishlists(juniors, teamLeads);
            if (teamLeadsWishlists == null) teamLeadsWishlists = WishlistGenerator.GenerateWishlists(teamLeads, juniors);
            this.Wishlists = juniorsWishlists.Concat(teamLeadsWishlists);

            // creating teams by wishlists
            var teams = manager.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);
            this.Teams = teams;
            return director.CountScore(teamLeads, juniors, teams, teamLeadsWishlists, juniorsWishlists);
        }
    }
}