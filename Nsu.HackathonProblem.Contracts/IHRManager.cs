namespace Nsu.HackathonProblem.Contracts
{
    public interface IHRManager
    {
        IEnumerable<Team> BuildTeams(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors,
            IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists);
    }
}
