using Mapster;
using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.Dto;

namespace Nsu.HackathonProblem.Mapper;
public class RegisterMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        TypeAdapterConfig<Junior, EmployeeDto>
                .NewConfig()
                .Map(dest => dest.Role, src => "Junior");

        TypeAdapterConfig<TeamLead, EmployeeDto>
            .NewConfig()
            .Map(dest => dest.Role, src => "TeamLead");

        TypeAdapterConfig<Employee, EmployeeDto>
            .NewConfig()
            .Map(dest => dest.Role, src => "");
    }
}