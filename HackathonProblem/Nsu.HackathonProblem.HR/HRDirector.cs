using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.Utils;

namespace Nsu.HackathonProblem.HR
{
    public class HRDirector(IHarmonicCounter harmonicCounter)
    {
        public double CountScore(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors,
            IEnumerable<Team> teams, IEnumerable<Wishlist> teamLeadsWishlists,
            IEnumerable<Wishlist> juniorsWishlists)
        {
            List<int> allIndexes = CountSatisfactionIndexes(juniors, teams, juniorsWishlists).ToList();
            allIndexes.AddRange(CountSatisfactionIndexes(teamLeads, teams, teamLeadsWishlists).ToList());

            return harmonicCounter.CountHarmonic(allIndexes);
        }

        private int[] CountSatisfactionIndexes(IEnumerable<Employee> employees, IEnumerable<Team> teams, IEnumerable<Wishlist> wishlists)
        {
            var indexes = new int[employees.Count()];
            foreach (Employee employee in employees)
            {
                indexes[employee.Id - 1] = 0;
                foreach (Team team in teams)
                {
                    Employee t = team.TeamLead;
                    Employee j = team.Junior;
                    if ((employee is TeamLead && t.Id == employee.Id)
                         || (employee is Junior && j.Id == employee.Id))
                    {
                        var wishlist = wishlists.FirstOrDefault(w => w.Employee.Id == employee.Id);
                        if (wishlist == null)
                            continue;
                        var rating = wishlist.DesiredEmployees;
                        int teammateId = (employee is TeamLead ? j.Id : t.Id);
                        int teammateIndex = Array.FindIndex(rating, id => id == teammateId);
                        if (teammateIndex == -1)
                            continue;
                        indexes[employee.Id - 1] = employees.Count() - teammateIndex;
                    }
                }
            }
            return indexes;
        }
    }
}
