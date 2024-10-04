using Nsu.HackathonProblem.Strategies;
using Nsu.HackathonProblem.Contracts;

namespace Nsu.HackathonProblem.HR
{
    public class HRManager(ITeamBuildingStrategy concreteStrategy)
    {
        ITeamBuildingStrategy strategy = concreteStrategy;
        public IEnumerable<Team> BuildTeams(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors,
            IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists)
        {
            if (teamLeads.Count() != juniors.Count())
                throw new ArgumentException("The number of teamleads and juniors should be the same");
            return strategy.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);
        }
    }
}