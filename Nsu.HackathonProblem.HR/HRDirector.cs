using System.Collections.Generic;
using System.Linq;
using Nsu.HackathonProblem.Contracts;

namespace Nsu.HackathonProblem.HR
{
    public class HRDirector
    {
        enum EmployeeLevel
        {
            TeamLead,
            Junior
        }

        public double countScore(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors, IEnumerable<Team> teams,
            IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists)
        {
            double sum = 0.0;
            // add satisfaction indexes for juniors
            int[] indexes = CountSatisfactionIndexes(EmployeeLevel.Junior, juniors, teams, juniorsWishlists);
            for (int i = 1; i < indexes.Length; ++i)
            {
                if (indexes[i] != 0.0) sum += 1.0 / indexes[i];
            }
            // add satisfaction indexes for teamLeads
            indexes = CountSatisfactionIndexes(EmployeeLevel.TeamLead, teamLeads, teams, teamLeadsWishlists);
            for (int i = 1; i < indexes.Length; ++i)
            {
                if (indexes[i] != 0.0) sum += 1.0 / indexes[i];
            }

            return (teamLeads.Count() + juniors.Count()) / sum;
        }

        static int[] CountSatisfactionIndexes(EmployeeLevel employeeLevel, IEnumerable<Employee> employees, IEnumerable<Team> teams, IEnumerable<Wishlist> wishlists)
        {
            var indexes = new int[employees.Count() + 1];
            foreach (Employee employee in employees)
            {
                indexes[employee.Id] = 0;
                foreach (Team team in teams)
                {
                    Employee t;
                    Employee j;
                    team.Deconstruct(out t, out j);
                    if ((employeeLevel == EmployeeLevel.TeamLead && t.Id == employee.Id)
                         || (employeeLevel == EmployeeLevel.Junior && j.Id == employee.Id))
                    {
                        var wishlist = wishlists.FirstOrDefault(w => w.EmployeeId == employee.Id);
                        if (wishlist == null) continue;
                        var rating = wishlist.DesiredEmployees;
                        int teammateId = (employeeLevel == EmployeeLevel.TeamLead ? j.Id : t.Id);
                        int teammateIndex = findTeammateIndex(rating, teammateId);
                        if (teammateIndex == -1) continue;
                        indexes[employee.Id] = employees.Count() - teammateIndex;
                    }
                }
            }
            return indexes;
        }

        static int findTeammateIndex(int[] rating, int id)
        {
            for (int i = 0; i < rating.Length; i++)
            {
                if (rating[i] == id) return i;
            }
            return -1;
        }
    }
}
