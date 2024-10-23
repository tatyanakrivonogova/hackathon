using System.ComponentModel.DataAnnotations;
namespace Nsu.HackathonProblem.Dto;

public class EmployeeDto
{
    public int EmployeePk { get; set; }
    public int Id { get; set; }
    required public string Name { get; set; }
    required public string Role { get; set; }
}