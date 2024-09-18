namespace Nsu.HackathonProblem.Contracts
{
    public record Team(Employee TeamLead, Employee Junior)
    {
        public void Deconstruct(out Employee teamLead, out Employee junior)
        {
            teamLead = this.TeamLead;
            junior = this.Junior;
        }
    }
}
