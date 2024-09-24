using Microsoft.Extensions.DependencyInjection; 
using Microsoft.Extensions.Hosting; 
using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.Strategies;
using Nsu.HackathonProblem.HR;

namespace Nsu.HackathonProblem.HackathonProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            // var host = Host.CreateDefaultBuilder(args) 
            //         .ConfigureServices((hostContext, services) => 
            //     { 
            //             services.AddHostedService<Experiment>(); 
            //             services.AddTransient<Hackathon>(_ => new Hackathon()); 
            //             services.AddTransient<ITeamBuildingStrategy, BaseTeamBuildingStrategy>(); 
            //             services.AddTransient<HRManager>();
            //             services.AddTransient<HRDirector>(); 
            //     }).Build(); 
            // host.Run(); 
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<Experiment>();
                    services.AddTransient<Hackathon>(_ => new Hackathon()); 
                    services.AddTransient<ITeamBuildingStrategy, BaseTeamBuildingStrategy>(); 
                    services.AddTransient<HRManager>();
                    services.AddTransient<HRDirector>(); 
                })
                .Build();
            var experiment = host.Services.GetService<Experiment>();
            experiment?.Run();
        }
    }
}