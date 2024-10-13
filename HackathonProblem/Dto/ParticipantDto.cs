namespace Nsu.HackathonProblem.Dto;

public class ParticipantDto()
{
    public int EmployeePk { get; set; }
    public int HackathonId { get; set; }
    public EmployeeDto Employee { get; set; }
    public HackathonDto Hackathon { get; set; }
}