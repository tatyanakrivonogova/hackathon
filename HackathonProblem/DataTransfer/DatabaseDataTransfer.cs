using Nsu.HackathonProblem.Contracts;

namespace Nsu.HackathonProblem.DataTransfer
{
    public class DatabaseDataTransfer : IDataTransfer
    {
        public void saveData(Hackathon hackathon)
        {
            Mapper.Initialize();
            using (var context = new HackathonContext())
            {
                context.Hackathon.Add(Mapper.MapHackathonToHackathonDto(hackathon));
                context.SaveChanges();
            }
        }
    }
}
