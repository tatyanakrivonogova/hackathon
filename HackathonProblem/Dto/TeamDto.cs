using System.ComponentModel.DataAnnotations;
namespace Nsu.HackathonProblem.Dto;

public class TeamDto()
{
    [Key] public int Ident { get; set; }
    public int TeamLeadId { get; set; }
    public int JuniorId { get; set; }
}