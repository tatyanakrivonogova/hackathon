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

        public List<Hackathon> loadData()
        {
            using (var context = new HackathonContext())
            {
                var hackathonDtos = context.Hackathon.ToList();
                var hackathons = new List<Hackathon>();
                foreach (var hackathonDto in hackathonDtos)
                {
                    hackathonDto.Employees = context.Employee
                                             .ToList()
                                             .Where(e => e.HackathonId == hackathonDto.Id)
                                             .ToList();
                    hackathonDto.Wishlists = context.Wishlist
                                             .ToList()
                                             .Where(w => w.HackathonId == hackathonDto.Id)
                                             .ToList();
                    hackathonDto.Teams = context.Team
                                             .ToList()
                                             .Where(t => t.HackathonId == hackathonDto.Id)
                                             .ToList();
                    hackathons.Add(Mapper.MapHackathonDtoToHackathon(hackathonDto));
                }
                return hackathons;
            }
        }
    }
}
