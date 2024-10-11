using Nsu.HackathonProblem.Contracts;

namespace Nsu.HackathonProblem.DataTransfer
{
    public interface IDataTransfer
    {
        void saveData(Hackathon hackathon);
        List<Hackathon> loadData();
    }
}
