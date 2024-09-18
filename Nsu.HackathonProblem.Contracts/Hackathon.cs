using Nsu.HackathonProblem.Utils;
using Nsu.HackathonProblem.HR;

namespace Nsu.HackathonProblem.Contracts
{
    public class Hackathon
    {
        public double RunHackathon(HRManager manager, HRDirector director, 
                IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors)
        {
            // generate wishlists randomly
            var juniorsWishlists = WishlistGenerator.GenerateWishlists(juniors, teamLeads);
            var teamLeadsWishlists = WishlistGenerator.GenerateWishlists(teamLeads, juniors);

            // creating teams by wishlists
            var teams = manager.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);
            double score = director.CountScore(teamLeads, juniors, teams, juniorsWishlists, teamLeadsWishlists);
            return score;
        }
    }
}