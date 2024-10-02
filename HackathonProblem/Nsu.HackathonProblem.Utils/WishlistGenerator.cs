using Nsu.HackathonProblem.Contracts;

namespace Nsu.HackathonProblem.Utils
{
    public static class WishlistGenerator
    {
        public static IEnumerable<Wishlist> GenerateWishlists(IEnumerable<Employee> employees, IEnumerable<Employee> others)
        {
            Random rand = new Random();
            return employees.Select(e => new Wishlist(e.Id, others.Select(o => o.Id).OrderBy(x => rand.Next()).ToArray()));
        }
    }
}
