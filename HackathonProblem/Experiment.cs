using Microsoft.Extensions.Options;

using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.HR;
using Nsu.HackathonProblem.Strategies;
using Nsu.HackathonProblem.Utils;
using Nsu.HackathonProblem.DataTransfer;

class Experiment(HRDirector director, HRManager manager, 
                 IOptions<HackathonOptions> hackathonOptions,
                 IDataTransfer dataTransfer)
{
    public void Run()
    {
        HackathonOptions options = hackathonOptions.Value;

        // reading juniors
        var juniors = EmployeesReader.ReadJuniors(options.juniorsFile);
        // reading teamLeads
        var teamLeads = EmployeesReader.ReadTeamLeads(options.teamLeadsFile);

        double sumScore = 0.0;

        Hackathon hackathon = new Hackathon();
        for (int i = 0; i < options.hackathonRepeats; i++)
        {
            double score = hackathon.RunHackathon(manager, director, teamLeads, juniors);
            hackathon.Score = score;
            Console.WriteLine($"score [i={i}]: {score}");
            sumScore += score;
            dataTransfer.SaveHackathon(hackathon, options);
        }

        Console.WriteLine($"Average score for {options.hackathonRepeats} hackathons: {sumScore / options.hackathonRepeats}");

        var allHackathons = dataTransfer.LoadAllHackathons(options);
        double allScoresSum = 0.0;
        foreach (Hackathon h in allHackathons)
        {
            allScoresSum += hackathon.Score;
        }
        Console.WriteLine($"Average score for all {allHackathons.Count()} hackathons: {allScoresSum / allHackathons.Count()}");

        int selectedId = 1; // for example
        var selected = dataTransfer.LoadHackathonById(selectedId, options);
        if (selected != null)
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine($"Hackathon: {selected.Id}, score: {selected.Score}");

            if (selected.Wishlists == null)
            {
                Console.WriteLine("Wishlist are not found");
            } else
            {
                foreach (var w in selected.Wishlists)
                {
                    Console.WriteLine($"wishlist: {w.Employee}, desiredEmployees: {w.DesiredEmployees[0]}, {w.DesiredEmployees[1]}, {w.DesiredEmployees[2]}, {w.DesiredEmployees[3]}, {w.DesiredEmployees[4]}");
                }
            }
            
            if (selected.Teams == null)
            {
                Console.WriteLine("Teams are not found");
            } else
            {
                foreach (var t in selected.Teams)
                {
                    Console.WriteLine($"team: {t.Junior}, {t.TeamLead}");
                }
            }
        } else 
        {
            Console.WriteLine("Hackathon is not found");
        }
        
    }
}