using Mapster;
using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.Dto;

namespace Nsu.HackathonProblem.Mapper;
public class RegisterMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        TypeAdapterConfig<Team, TeamDto>
                .NewConfig();

        TypeAdapterConfig<Junior, EmployeeDto>
                .NewConfig()
                .Map(dest => dest.Role, src => "Junior");

            TypeAdapterConfig<TeamLead, EmployeeDto>
                .NewConfig()
                .Map(dest => dest.Role, src => "TeamLead");

            TypeAdapterConfig<Wishlist, WishlistDto>
                .NewConfig()
                .Map(dest => dest.Employee, src => (src.Employee is Junior) 
                            ? (src.Employee as Junior).Adapt<EmployeeDto>() 
                            : (src.Employee as TeamLead).Adapt<EmployeeDto>());
    }
}