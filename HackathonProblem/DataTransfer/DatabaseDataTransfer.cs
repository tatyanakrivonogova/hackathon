using Microsoft.Extensions.Options;
using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.Dto;

namespace Nsu.HackathonProblem.DataTransfer
{
    public class DatabaseDataTransfer : IDataTransfer
    {
        HackathonOptions options;
        public DatabaseDataTransfer(IOptions<HackathonOptions> hackathonOptions)
        {
            options = hackathonOptions.Value;
        }
        
        private List<EmployeeDto> employeesList;
        public void saveData(List<Employee> juniors, List<Employee> teamleads)
        {
            Mapper.Initialize();
            using (var context = new HackathonContext(options.database))
            {
                employeesList = new List<EmployeeDto>();
                foreach (var j in juniors)
                {
                    employeesList.Add(Mapper.MapJuniorToEmployeeDto(j));
                }
                
                foreach (var t in teamleads)
                {
                    employeesList.Add(Mapper.MapTeamLeadToEmployeeDto(t));
                }
                context.Employee.AddRange(employeesList);
                context.SaveChanges();
            }
        }

        public void saveData(Hackathon hackathon)
        {
            Mapper.Initialize();
            using (var context = new HackathonContext(options.database))
            {
                context.Hackathon.Add(Mapper.MapHackathonToHackathonDto(hackathon, employeesList));
                context.SaveChanges();
            }
        }

        public List<Hackathon> loadData()
        {
            using (var context = new HackathonContext(options.database))
            {
                var hackathonDtos = context.Hackathon.ToList();
                var hackathons = new List<Hackathon>();
                var employees = context.Employee
                                             .ToList();

                foreach (var hackathonDto in hackathonDtos)
                {
                    hackathonDto.Participants = context.Participant
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
                    hackathons.Add(Mapper.MapHackathonDtoToHackathon(hackathonDto, employees));
                }
                return hackathons;
            }
        }
    }
}
