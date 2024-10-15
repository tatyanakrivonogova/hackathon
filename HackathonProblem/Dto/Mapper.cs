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

    public static HackathonDto MapHackathonToHackathonDto(Hackathon hackathon, List<EmployeeDto> employeesList)
    {
        if (hackathon.TeamLeads == null || hackathon.Juniors == null ||
            hackathon.Wishlists == null || hackathon.Teams == null) {
            throw new ArgumentException("Hackathon has empty fields");
        }
        // map participants
        var participantsList = new List<ParticipantDto>();
        foreach (var j in hackathon.Juniors)
        {
            participantsList.Add(MapJuniorToParticipantDto(j, employeesList));
        }

        var TeamleadsList = new List<ParticipantDto>();
        foreach (var t in hackathon.TeamLeads)
        {
            participantsList.Add(MapTeamLeadToParticipantDto(t, employeesList));
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
            Participants = participantsList, 
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

    public static ParticipantDto MapJuniorToParticipantDto(Employee junior, List<EmployeeDto> employeesList)
    {
        var participantDto = new ParticipantDto();
        EmployeeDto employee = employeesList.Where(e => e.Role == "Junior" && e.Id == junior.Id).FirstOrDefault();
        if (employee == null) {
            throw new ArgumentException("Employee for junior is not found");
        }
        participantDto.EmployeePk = employee.EmployeePk;
        return participantDto;
    }

    public static ParticipantDto MapTeamLeadToParticipantDto(Employee teamlead, List<EmployeeDto> employeesList)
    {
        var participantDto = new ParticipantDto();
        EmployeeDto employee = employeesList.Where(e => e.Role == "TeamLead" && e.Id == teamlead.Id).FirstOrDefault();
        if (employee == null) {
            throw new ArgumentException("Employee for teamlead is not found");
        }
        participantDto.EmployeePk = employee.EmployeePk;
        return participantDto;
    }

    public static WishlistDto MapWishlistToWishlistDto(Wishlist wishlist)
    {
        var wishlistDto = wishlist.Adapt<WishlistDto>();
        return wishlistDto;
    }

    public static TeamDto MapTeamToTeamDto(Team team)
    {
        var teamDto = new TeamDto();
        teamDto.JuniorId = team.Junior.Id;
        teamDto.TeamLeadId = team.TeamLead.Id;
        return teamDto;
    }

    public static Hackathon MapHackathonDtoToHackathon(HackathonDto hackathonDto, List<EmployeeDto> employees)
    {
        var hackathon = new Hackathon();
        hackathon.Id = hackathonDto.Id;
        hackathon.Score = hackathonDto.Score;

        // map employess
        var juniors = new List<Junior>();
        var teamLeads = new List<TeamLead>();
        foreach (var p in hackathonDto.Participants)
        {
            p.Employee = employees.FirstOrDefault(e => e.EmployeePk == p.EmployeePk);
            if (p.Employee == null)
            {
                continue;
            }
            if (p.Employee.Role == "Junior")
            {
                juniors.Add(MapEmployeeDtoToJunior(p.Employee));
            }
            if (p.Employee.Role == "TeamLead")
            {
                teamLeads.Add(MapEmployeeDtoToTeamLead(p.Employee));
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
        // return team;

        return new Team(new Junior(teamDto.JuniorId, ""), 
                        new TeamLead(teamDto.TeamLeadId, ""));
    }
}