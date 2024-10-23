using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.Utils;

public static class TestDataInitializer
{
    public static IEnumerable<Wishlist> GetJuniorsWishlist(List<Junior> juniors)
    {
        List<Wishlist> juniorsDesiredEmployees = new List<Wishlist>();
        juniorsDesiredEmployees.Add(new Wishlist(juniors[0], new int[] { 1, 2, 3, 4, 5 }));
        juniorsDesiredEmployees.Add(new Wishlist(juniors[1], new int[] { 1, 3, 5, 4, 2 }));
        juniorsDesiredEmployees.Add(new Wishlist(juniors[2], new int[] { 4, 5, 2, 1, 3 }));
        juniorsDesiredEmployees.Add(new Wishlist(juniors[3], new int[] { 2, 3, 1, 4, 5 }));
        juniorsDesiredEmployees.Add(new Wishlist(juniors[4], new int[] { 1, 2, 4, 5, 3 }));
        return juniorsDesiredEmployees;
    }

    public static IEnumerable<Wishlist> GetTeamLeadsWishlist(List<TeamLead> teamLeads)
    {
        List<Wishlist> teamLeadsDesiredEmployees = new List<Wishlist>();
        teamLeadsDesiredEmployees.Add(new Wishlist(teamLeads[0], new int[] { 2, 1, 4, 3, 5 }));
        teamLeadsDesiredEmployees.Add(new Wishlist(teamLeads[1], new int[] { 4, 3, 5, 1, 2 }));
        teamLeadsDesiredEmployees.Add(new Wishlist(teamLeads[2], new int[] { 4, 2, 5, 1, 3 }));
        teamLeadsDesiredEmployees.Add(new Wishlist(teamLeads[3], new int[] { 2, 3, 1, 5, 4 }));
        teamLeadsDesiredEmployees.Add(new Wishlist(teamLeads[4], new int[] { 4, 1, 2, 5, 3 }));
        return teamLeadsDesiredEmployees;
    }

    public static IEnumerable<Team> GetTeams(List<TeamLead> teamLeads, List<Junior> juniors)
    {
        List<Team> teams = new List<Team>();
        teams.Add(new Team(teamLeads[0], juniors[1]));
        teams.Add(new Team(teamLeads[1], juniors[2]));
        teams.Add(new Team(teamLeads[2], juniors[3]));
        teams.Add(new Team(teamLeads[3], juniors[4]));
        teams.Add(new Team(teamLeads[4], juniors[0]));
        return teams;
    }

    public static Hackathon GetHackathon()
    {
        string juniorsFile = "Juniors5.csv";
        string teamLeadsFile = "Teamleads5.csv";
        var juniors = EmployeesReader.ReadJuniors(juniorsFile);
        var teamLeads = EmployeesReader.ReadTeamLeads(teamLeadsFile);
        List<Wishlist> teamLeadsWishlists = GetTeamLeadsWishlist(teamLeads.ToList()).ToList();
        List<Wishlist> juniorsWishlists = GetJuniorsWishlist(juniors.ToList()).ToList();
        Hackathon hackathon = new Hackathon()
        {
            Score = 1.15,
            Wishlists = teamLeadsWishlists.Concat(juniorsWishlists),
            Teams = GetTeams(teamLeads.ToList(), juniors.ToList())
        };
        return hackathon;
    }
}