using Microsoft.Extensions.Options;
using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.HR;
using Nsu.HackathonProblem.Strategies;
using Nsu.HackathonProblem.Utils;

class Experiment(Hackathon hackathon, HRDirector director, HRManager manager, IOptions<ConstantOptions> options)
{
    public void Run()
    {
        ConstantOptions constants = options.Value;
        // reading juniors
        var juniors = EmployeesReader.ReadJuniors(constants.juniorsFile);

        // reading teamLeads
        var teamLeads = EmployeesReader.ReadTeamLeads(constants.teamLeadsFile);

        double sumScore = 0.0;
        for (int i = 0; i < constants.hackathonRepeats; i++)
        {
            double score = hackathon.RunHackathon(manager, director, teamLeads, juniors);
            Console.WriteLine($"score [i={i}]: {score}");
            sumScore += score;
        }

        Console.WriteLine($"Average score: {sumScore / constants.hackathonRepeats}");
    }
}