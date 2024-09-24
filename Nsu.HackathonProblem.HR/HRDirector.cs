using Nsu.HackathonProblem.Contracts;

namespace Nsu.HackathonProblem.HR
{
    public class HRDirector : IHRDirector
    {
        public double CountScore(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors, 
            IEnumerable<Team> teams, IEnumerable<Wishlist> teamLeadsWishlists, 
            IEnumerable<Wishlist> juniorsWishlists)
        {
            double sum = 0.0;
            // add satisfaction indexes for juniors
            int[] indexes = CountSatisfactionIndexes(juniors, teams, juniorsWishlists);
            for (int i = 1; i < indexes.Length; ++i) if (indexes[i] != 0.0) sum += 1.0 / indexes[i];
            // add satisfaction indexes for teamLeads
            indexes = CountSatisfactionIndexes(teamLeads, teams, teamLeadsWishlists);
            for (int i = 1; i < indexes.Length; ++i) if (indexes[i] != 0.0) sum += 1.0 / indexes[i];

            return (teamLeads.Count() + juniors.Count()) / sum;
        }

        private int[] CountSatisfactionIndexes(IEnumerable<Employee> employees, IEnumerable<Team> teams, IEnumerable<Wishlist> wishlists)
        {
            var indexes = new int[employees.Count() + 1];
            foreach (Employee employee in employees)
            {
                indexes[employee.Id] = 0;
                foreach (Team team in teams)
                {
                    Employee t = team.TeamLead;
                    Employee j = team.Junior;
                    if ((employee is TeamLead && t.Id == employee.Id)
                         || (employee is Junior && j.Id == employee.Id))
                    {
                        var wishlist = wishlists.FirstOrDefault(w => w.EmployeeId == employee.Id);
                        if (wishlist == null) continue;
                        var rating = wishlist.DesiredEmployees;
                        int teammateId = (employee is TeamLead ? j.Id : t.Id);
                        int teammateIndex = Array.FindIndex(rating, id => id == teammateId);
                        if (teammateIndex == -1) continue;
                        indexes[employee.Id] = employees.Count() - teammateIndex;
                    }
                }
            }
            return indexes;
        }
    }
}
