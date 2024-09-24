namespace Nsu.HackathonProblem.Contracts
{
    public interface IHRDirector
    {
        double CountScore(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors, 
            IEnumerable<Team> teams, IEnumerable<Wishlist> teamLeadsWishlists, 
            IEnumerable<Wishlist> juniorsWishlists);
    }
}
