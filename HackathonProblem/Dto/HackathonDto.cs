namespace Nsu.HackathonProblem.Dto;

public class HackathonDto()
{
    public int Id { get; set; }
    public double Score { get; init; }
    required public IEnumerable<EmployeeDto> Participants { get; init; }
    required public IEnumerable<WishlistDto> Wishlists { get; init; }
    required public IEnumerable<TeamDto> Teams { get; init; }
}