using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Mapster;

using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.Dto;

namespace Nsu.HackathonProblem.DataTransfer
{
    public class DatabaseDataTransfer : IDataTransfer
    {
        private HackathonContext context;
        public DatabaseDataTransfer(HackathonContext context)
        {
            this.context = context;
        }
        public void SaveHackathon(Hackathon hackathon) {
            HackathonDto hackathonDto = MapHackathon(hackathon);
            context.Hackathon.Add(hackathonDto);
            context.SaveChanges();
        }

        public List<Hackathon> LoadAllHackathons()
        {
            return context.Hackathon.Select(hackathonDto => hackathonDto.Adapt<Hackathon>()).ToList();
        }

        public Hackathon? LoadHackathonById(int id)
        {
            Hackathon? selected = context.Hackathon
                                        .Include("Wishlists.Employee")
                                        .Include("Teams.Junior")
                                        .Include("Teams.TeamLead")
                                        .Select(hackathonDto => hackathonDto.Adapt<Hackathon>())
                                        .ToList().Where(h => h.Id == id).FirstOrDefault();
            return selected;
        }

        private List<WishlistDto> MapWishlists(IEnumerable<Wishlist> wishlists, Dictionary<Employee, EmployeeDto> employeeCache)
        {
            List<WishlistDto> wishlistDtos = new List<WishlistDto>();
            foreach (Wishlist wishlist in wishlists)
            {
                WishlistDto wishlistDto = new WishlistDto()
                {
                    DesiredEmployees = wishlist.DesiredEmployees
                };
                if (!employeeCache.ContainsKey(wishlist.Employee))
                {
                    employeeCache[wishlist.Employee] = (wishlist.Employee is Junior)
                                                        ? (wishlist.Employee as Junior).Adapt<EmployeeDto>()
                                                        : (wishlist.Employee as TeamLead).Adapt<EmployeeDto>();
                }
                wishlistDto.Employee = employeeCache[wishlist.Employee];
                wishlistDtos.Add(wishlistDto);
            }
            return wishlistDtos;
        }

        private List<TeamDto> MapTeams(IEnumerable<Team> teams, Dictionary<Employee, EmployeeDto> employeeCache)
        {
            List<TeamDto> teamDtos = new List<TeamDto>();
            foreach (Team team in teams)
            {
                TeamDto teamDto = new TeamDto();
                if (!employeeCache.ContainsKey(team.Junior))
                {
                    employeeCache[team.Junior] = (team.Junior as Junior).Adapt<EmployeeDto>();
                }
                teamDto.Junior = employeeCache[team.Junior];
                if (!employeeCache.ContainsKey(team.TeamLead))
                {
                    employeeCache[team.TeamLead] = (team.TeamLead as TeamLead).Adapt<EmployeeDto>();
                }
                teamDto.TeamLead = employeeCache[team.TeamLead];
                teamDtos.Add(teamDto);
            }
            return teamDtos;
        }
        private HackathonDto MapHackathon(Hackathon hackathon)
        {
            if (hackathon.Wishlists == null || hackathon.Teams == null)
            {
                throw new ArgumentException("Wishlists or teams for hackathon are undefined");
            }

            var employeeCache = new Dictionary<Employee, EmployeeDto>();
            HackathonDto hackathonDto = new HackathonDto()
            {
                Id = hackathon.Id,
                Score = hackathon.Score,
                Wishlists = MapWishlists(hackathon.Wishlists, employeeCache),
                Teams = MapTeams(hackathon.Teams, employeeCache)
            };
            return hackathonDto;
        }
    }
}