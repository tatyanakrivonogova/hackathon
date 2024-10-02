using Nsu.HackathonProblem.Contracts;

namespace Nsu.HackathonProblem.HR
{
    public class HRDirector
    {
        public double CountScore(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors,
            IEnumerable<Team> teams, IEnumerable<Wishlist> teamLeadsWishlists,
            IEnumerable<Wishlist> juniorsWishlists)
        {
            List<int> allIndexes = CountSatisfactionIndexes(juniors, teams, juniorsWishlists).ToList();
            allIndexes.AddRange(CountSatisfactionIndexes(teamLeads, teams, teamLeadsWishlists).ToList());

            return CountAverageHarmonic(allIndexes);
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
                        var wishlist = wishlists.FirstOrDefault(w => w.EmployeeId == employee.Id);
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

        public double CountAverageHarmonic(List<int> allIndexes)
        {
            double sum = 0.0;
            foreach (int index in allIndexes)
                if (index != 0.0)
                    sum += 1.0 / index;

            return (allIndexes.Count()) / sum;
        }
    }
}
