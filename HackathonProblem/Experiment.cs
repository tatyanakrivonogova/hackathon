using Microsoft.Extensions.Options;
using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.HR;
using Nsu.HackathonProblem.Strategies;
using Nsu.HackathonProblem.Utils;
using Nsu.HackathonProblem.Dto;
using Nsu.HackathonProblem.DataTransfer;

class Experiment(Hackathon hackathon, HRDirector director, HRManager manager, 
                 IDataTransfer dataTransfer, IOptions<HackathonOptions> hackathonOptions)
{
    public void Run()
    {
        HackathonOptions options = hackathonOptions.Value;

        // reading juniors
        var juniors = EmployeesReader.ReadJuniors(options.juniorsFile);
        hackathon.Juniors = juniors;

        // reading teamLeads
        var teamLeads = EmployeesReader.ReadTeamLeads(options.teamLeadsFile);
        hackathon.TeamLeads = teamLeads;

        double sumScore = 0.0;
        for (int i = 0; i < options.hackathonRepeats; i++)
        {
            double score = hackathon.RunHackathon(manager, director, teamLeads, juniors);
            hackathon.Score = score;
            Console.WriteLine($"score [i={i}]: {score}");
            sumScore += score;
            dataTransfer.saveData(hackathon);

        }

        Console.WriteLine($"Average score: {sumScore / options.hackathonRepeats}");

        // List<Hackathon> allHackathons = dataTransfer.loadData();
    }
}