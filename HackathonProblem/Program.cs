using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Mapster;

using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.Strategies;
using Nsu.HackathonProblem.HR;
using Nsu.HackathonProblem.Utils;
using Nsu.HackathonProblem.Mapper;
using Nsu.HackathonProblem.DataTransfer;

namespace Nsu.HackathonProblem.HackathonProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            new RegisterMapper().Register(new TypeAdapterConfig());
            
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddOptions<HackathonOptions>().Bind
                        (context.Configuration.GetSection("Hackathon"));
                    services.AddHostedService<ExperimentWorker>()
                        .AddSingleton<Experiment>()
                        .AddTransient<ITeamBuildingStrategy, BaseTeamBuildingStrategy>()
                        .AddTransient<HRManager>()
                        .AddTransient<HRDirector>()
                        .AddTransient<IHarmonicCounter, HarmonicMeanCounter>()
                        .AddSingleton<IDataTransfer, DatabaseDataTransfer>()
                        .BuildServiceProvider();
                }).Build();
            host.Run();
        }
    }
}