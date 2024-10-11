using Mapster;

using Nsu.HackathonProblem.Dto;
using Nsu.HackathonProblem.Contracts;

public static class Mapper
{
    public static void Initialize()
    {
        TypeAdapterConfig<Hackathon, HackathonDto>.NewConfig();
        TypeAdapterConfig<Junior, EmployeeDto>.NewConfig();
        TypeAdapterConfig<TeamLead, EmployeeDto>.NewConfig();
        TypeAdapterConfig<Wishlist, WishlistDto>.NewConfig();
        TypeAdapterConfig<Team, TeamDto>.NewConfig();
    }

    public static HackathonDto MapHackathonToHackathonDto(Hackathon hackathon)
    {
        // map employees
        var employeesList = new List<EmployeeDto>();
        foreach (var j in hackathon.Juniors)
        {
            employeesList.Add(MapJuniorToEmployeeDto(j));
        }

        var TeamleadsList = new List<EmployeeDto>();
        foreach (var t in hackathon.TeamLeads)
        {
            employeesList.Add(MapTeamLeadToEmployeeDto(t));
        }

        // map wishlists
        var wishlistsList = new List<WishlistDto>();
        foreach (var w in hackathon.Wishlists)
        {
            wishlistsList.Add(MapWishlistToWishlistDto(w));
        }

        // map teams
        var teamsList = new List<TeamDto>();
        foreach (var t in hackathon.Teams)
        {
            teamsList.Add(MapTeamToTeamDto(t));
        }
        return new HackathonDto {
            Score = hackathon.Score, 
            Employees = employeesList, 
            Wishlists = wishlistsList, 
            Teams = teamsList };
    }

    public static EmployeeDto MapJuniorToEmployeeDto(Employee junior)
    {
        var employeeDto = junior.Adapt<EmployeeDto>();
        employeeDto.Role = "Junior";
        return employeeDto;
    }

    public static EmployeeDto MapTeamLeadToEmployeeDto(Employee teamlead)
    {
        var employeeDto = teamlead.Adapt<EmployeeDto>();
        employeeDto.Role = "TeamLead";
        return employeeDto;
    }

    public static WishlistDto MapWishlistToWishlistDto(Wishlist wishlist)
    {
        var wishlistDto = wishlist.Adapt<WishlistDto>();
        return wishlistDto;
    }

    public static TeamDto MapTeamToTeamDto(Team team)
    {
        var teamDto = team.Adapt<TeamDto>();
        return teamDto;
    }

    public static Hackathon MapHackathonDtoToHackathon(HackathonDto hackathonDto)
    {
        var hackathon = new Hackathon();
        hackathon.Id = hackathonDto.Id;
        hackathon.Score = hackathonDto.Score;

        // map employess
        var juniors = new List<Junior>();
        var teamLeads = new List<TeamLead>();
        // Console.WriteLine($"----------------{hackathonDto.Employees}----------------");
        foreach (var e in hackathonDto.Employees)
        {
            if (e.Role == "Junior")
            {
                juniors.Add(MapEmployeeDtoToJunior(e));
            }
            if (e.Role == "TeamLead")
            {
                teamLeads.Add(MapEmployeeDtoToTeamLead(e));
            }
        }
        hackathon.Juniors = juniors;
        hackathon.TeamLeads = teamLeads;

        //  map wishlists
        var wishlists = new List<Wishlist>();
        foreach (var w in hackathonDto.Wishlists)
        {
            wishlists.Add(MapWishlistDtoToWishlist(w));
        }
        hackathon.Wishlists = wishlists;

        //  map teams
        var teams = new List<Team>();
        foreach (var t in hackathonDto.Teams)
        {
            teams.Add(MapTeamDtoToTeam(t));
        }
        hackathon.Teams = teams;
        return hackathon;
    }

    public static Junior MapEmployeeDtoToJunior(EmployeeDto employeeDto)
    {
        var junior = employeeDto.Adapt<Junior>();
        return junior;
    }

    public static TeamLead MapEmployeeDtoToTeamLead(EmployeeDto employeeDto)
    {
        var teamlead = employeeDto.Adapt<TeamLead>();
        return teamlead;
    }

    public static Wishlist MapWishlistDtoToWishlist(WishlistDto wishlistDto)
    {
        var wishlist = wishlistDto.Adapt<Wishlist>();
        return wishlist;
    }

    public static Team MapTeamDtoToTeam(TeamDto teamDto)
    {
        // var team = teamDto.Adapt<Team>();

        return new Team(new Junior(teamDto.JuniorId, ""), 
                        new TeamLead(teamDto.TeamLeadId, ""));
    }
}