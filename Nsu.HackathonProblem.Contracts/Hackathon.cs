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

            foreach (var jw in juniorsWishlists)
            {
                Console.WriteLine($"junior_id: {jw.EmployeeId}, wishlist: {jw.DesiredEmployees[0]}, {jw.DesiredEmployees[1]}, {jw.DesiredEmployees[2]}, {jw.DesiredEmployees[3]}, {jw.DesiredEmployees[4]}");
            }

            foreach (var tw in teamLeadsWishlists)
            {
                Console.WriteLine($"teamLead_id: {tw.EmployeeId}, wishlist: {tw.DesiredEmployees[0]}, {tw.DesiredEmployees[1]}, {tw.DesiredEmployees[2]}, {tw.DesiredEmployees[3]}, {tw.DesiredEmployees[4]}");
            }

            // creating teams by wishlists
            var teams = manager.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);

            foreach (var team in teams)
            {
                Console.WriteLine($"Team Lead: {team.TeamLead.Name}, Junior: {team.Junior.Name}");
            }
        
            double score = director.countScore(teamLeads, juniors, teams, juniorsWishlists, teamLeadsWishlists);
            return score;
        }
    }
}