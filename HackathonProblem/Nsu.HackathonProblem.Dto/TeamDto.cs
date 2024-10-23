using System.ComponentModel.DataAnnotations;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Nsu.HackathonProblem.Dto;
public class TeamDto()
{
    public int TeamPk { get; set; }
    public virtual EmployeeDto? Junior { get; set; }
    public virtual EmployeeDto? TeamLead { get; set; }
    public HackathonDto? Hackathon { get; set; }
}