using Nsu.HackathonProblem.Contracts;

namespace Nsu.HackathonProblem.DataTransfer
{
    public interface IDataTransfer
    {
        void SaveHackathon(Hackathon hackathon);
        List<Hackathon> LoadAllHackathons();
        Hackathon? LoadHackathonById(int id);
    }
}
