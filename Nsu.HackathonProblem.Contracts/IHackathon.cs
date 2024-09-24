namespace Nsu.HackathonProblem.Contracts
{
    public interface IHackathon
    {
        double RunHackathon(IHRManager manager, IHRDirector director, 
                IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors);
    }
}
