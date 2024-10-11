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
        // map participants
        var participantsList = new List<EmployeeDto>();
        foreach (var j in hackathon.Juniors)
        {
            participantsList.Add(MapJuniorToEmployeeDto(j));
        }

        var TeamleadsList = new List<EmployeeDto>();
        foreach (var t in hackathon.TeamLeads)
        {
            participantsList.Add(MapTeamLeadToEmployeeDto(t));
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
}