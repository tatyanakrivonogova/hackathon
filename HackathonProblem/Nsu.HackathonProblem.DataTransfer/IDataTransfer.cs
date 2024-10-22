using Nsu.HackathonProblem.Contracts;

namespace Nsu.HackathonProblem.DataTransfer
{
    public interface IDataTransfer
    {
        void SaveHackathon(Hackathon hackathon, HackathonOptions option);
        List<Hackathon> LoadAllHackathons(HackathonOptions option);
        Hackathon? LoadHackathonById(int id, HackathonOptions option);
    }
}
