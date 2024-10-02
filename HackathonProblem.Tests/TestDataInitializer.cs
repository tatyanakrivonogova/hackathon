using Nsu.HackathonProblem.Contracts;
public static class TestDataInitializer
{
    public static IEnumerable<Wishlist> GetJuniorsWishlist()
    {
        List<Wishlist> juniorsDesiredEmployees = new List<Wishlist>();
        juniorsDesiredEmployees.Add(new Wishlist(1, new int[] { 1, 2, 3, 4, 5 }));
        juniorsDesiredEmployees.Add(new Wishlist(2, new int[] { 1, 3, 5, 4, 2 }));
        juniorsDesiredEmployees.Add(new Wishlist(3, new int[] { 4, 5, 2, 1, 3 }));
        juniorsDesiredEmployees.Add(new Wishlist(4, new int[] { 2, 3, 1, 4, 5 }));
        juniorsDesiredEmployees.Add(new Wishlist(5, new int[] { 1, 2, 4, 5, 3 }));
        return juniorsDesiredEmployees;
    }

    public static IEnumerable<Wishlist> GetTeamLeadsWishlist()
    {
        List<Wishlist> teamLeadsDesiredEmployees = new List<Wishlist>();
        teamLeadsDesiredEmployees.Add(new Wishlist(1, new int[] { 2, 1, 4, 3, 5 }));
        teamLeadsDesiredEmployees.Add(new Wishlist(2, new int[] { 4, 3, 5, 1, 2 }));
        teamLeadsDesiredEmployees.Add(new Wishlist(3, new int[] { 4, 2, 5, 1, 3 }));
        teamLeadsDesiredEmployees.Add(new Wishlist(4, new int[] { 2, 3, 1, 5, 4 }));
        teamLeadsDesiredEmployees.Add(new Wishlist(5, new int[] { 4, 1, 2, 5, 3 }));
        return teamLeadsDesiredEmployees;
    }
}