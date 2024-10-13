using Nsu.HackathonProblem.Contracts;

namespace Nsu.HackathonProblem.DataTransfer
{
    public interface IDataTransfer
    {
        void saveData(List<Employee> juniors, List<Employee> teamleads);
        void saveData(Hackathon hackathon);
        List<Hackathon> loadData();
    }
}
