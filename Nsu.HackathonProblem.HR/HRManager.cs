using Nsu.HackathonProblem.Strategies;
using Nsu.HackathonProblem.Contracts;

namespace Nsu.HackathonProblem.HR
{
    public class HRManager(ITeamBuildingStrategy concreteStrategy) : IHRManager
    {
        ITeamBuildingStrategy strategy = concreteStrategy;
        public IEnumerable<Team> BuildTeams(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors,
            IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists)
        {
            return strategy.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);
        }
    }
}