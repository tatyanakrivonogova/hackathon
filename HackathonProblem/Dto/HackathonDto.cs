namespace Nsu.HackathonProblem.Dto;

public class HackathonDto()
{
    public int Id { get; set; }
    public double Score { get; set; }
    required public IEnumerable<ParticipantDto> Participants { get; set; }
    required public IEnumerable<WishlistDto> Wishlists { get; set; }
    required public IEnumerable<TeamDto> Teams { get; set; }
}