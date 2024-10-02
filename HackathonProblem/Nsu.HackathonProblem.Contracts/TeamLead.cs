namespace Nsu.HackathonProblem.Contracts
{
    public record TeamLead(int Id, string Name) : Employee(Id, Name);
}
