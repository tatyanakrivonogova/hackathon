using System.ComponentModel.DataAnnotations;
namespace Nsu.HackathonProblem.Dto;

public class WishlistDto()
{
    [Key] public int WishlistPk { get; set; }
    public int EmployeeId { get; set; }
    required public int[] DesiredEmployees { get; set; }
    public int HackathonId { get; set; }
    public HackathonDto? Hackathon { get; set; }
}