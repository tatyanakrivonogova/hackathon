using System.ComponentModel.DataAnnotations;
using Mapster;

namespace Nsu.HackathonProblem.Dto;
public class TeamDto()
{
    public int TeamLeadId { get; set; }
    public int JuniorId { get; set; }
    public int HackathonId { get; set; }
    public virtual EmployeeDto? Junior { get; set; }
    public virtual EmployeeDto? TeamLead { get; set; }
    public HackathonDto? Hackathon { get; set; }
}