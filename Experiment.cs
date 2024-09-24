using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.HR;
using Nsu.HackathonProblem.Strategies;
using Nsu.HackathonProblem.Utils;

class Experiment(IHackathon hackathon, IHRDirector director, IHRManager manager)
{
    public void Run()
    {
        // reading juniors
        var juniors = EmployeesReader.ReadJuniors(Constants.juniorsFile);

        // reading teamLeads
        var teamLeads = EmployeesReader.ReadTeamLeads(Constants.teamLeadsFile);

        // HRManager manager = new HRManager(new BaseTeamBuildingStrategy());
        // HRDirector director = new HRDirector();
        // Hackathon hackathon = new Hackathon();
        double sumScore = 0.0;
        for (int i = 0; i < Constants.hackathonRepeats; i++)
        {
            double score = hackathon.RunHackathon(manager, director, teamLeads, juniors);
            Console.WriteLine($"score [i={i}]: {score}");
            sumScore += score;
        }

        Console.WriteLine($"Average score: {sumScore / Constants.hackathonRepeats}");
    }
}