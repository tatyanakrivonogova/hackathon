namespace Nsu.HackathonProblem.Dto;

public class HackathonDto()
{
    public int Id { get; set; }
    public double Score { get; set; }
    required public List<WishlistDto> Wishlists { get; set; }
    required public List<TeamDto> Teams { get; set; }
}