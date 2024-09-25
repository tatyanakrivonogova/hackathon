using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.Strategies;
using Nsu.HackathonProblem.HR;

namespace Nsu.HackathonProblem.HackathonProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddOptions<ConstantOptions>().Bind
                        (context.Configuration.GetSection("Constants"));
                    services.AddHostedService<ExperimentWorker>()
                    .AddSingleton<Experiment>()
                    .AddTransient<Hackathon>()
                    .AddTransient<ITeamBuildingStrategy, BaseTeamBuildingStrategy>()
                    .AddTransient<HRManager>()
                    .AddTransient<HRDirector>()
                    .BuildServiceProvider();
                })
                .Build();
            host.Run();
        }
    }
}