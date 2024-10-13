using System.ComponentModel.DataAnnotations;
namespace Nsu.HackathonProblem.Dto;

public class EmployeeDto
{
    // [Key] public int Ident { get; set; }
    public int Id { get; set; }
    required public string Name { get; set; }
    required public string Role { get; set; } // "TeamLead" || "Junior"
    public int HackathonId { get; set; }
    public HackathonDto Hackathon { get; set; }
}