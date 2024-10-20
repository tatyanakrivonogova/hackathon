using System.ComponentModel.DataAnnotations;
using Mapster;

namespace Nsu.HackathonProblem.Dto;
public class WishlistDto()
{
    [Key] public int WishlistPk { get; set; }
    required public int[] DesiredEmployees { get; set; }
    public virtual EmployeeDto? Employee { get; set; }
    public HackathonDto? Hackathon { get; set; }
}