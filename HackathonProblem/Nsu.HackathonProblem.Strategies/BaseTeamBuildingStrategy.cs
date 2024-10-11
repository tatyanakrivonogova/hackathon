using Nsu.HackathonProblem.Contracts;

namespace Nsu.HackathonProblem.Strategies
{
    public class BaseTeamBuildingStrategy : ITeamBuildingStrategy
    {
        public IEnumerable<Team> BuildTeams(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors,
            IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists)
        {
            var teams = new List<Team>();

            foreach (var teamLead in teamLeads)
            {
                var wishlist = teamLeadsWishlists.FirstOrDefault(w => w.EmployeeId == teamLead.Id);
                if (wishlist == null) 
                    continue;

                foreach (var desiredId in wishlist.DesiredEmployees)
                {
                    var junior = juniors.FirstOrDefault(j => j.Id == desiredId);
                    if (junior != null && !teams.Any(t => t.TeamLead.Id == teamLead.Id || t.Junior.Id == junior.Id))
                    {
                        teams.Add(new Team(teamLead, junior));
                        break;
                    }
                }
            }
            return teams;
        }
    }
}
