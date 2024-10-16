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

        dataTransfer.saveData(juniors.ToList(), teamLeads.ToList());

        double sumScore = 0.0;
        for (int i = 0; i < options.hackathonRepeats; i++)
        {
            double score = hackathon.RunHackathon(manager, director, teamLeads, juniors);
            hackathon.Score = score;
            Console.WriteLine($"score [i={i}]: {score}");
            sumScore += score;
            dataTransfer.saveData(hackathon);
        }

        Console.WriteLine($"Average score for {options.hackathonRepeats} hackathons: {sumScore / options.hackathonRepeats}");

        List<Hackathon> allHackathons = dataTransfer.loadData();
        double allScoresSum = 0.0;
        foreach (Hackathon hackathon in allHackathons)
        {
            allScoresSum += hackathon.Score;
        }
        Console.WriteLine($"Average score for all {allHackathons.Count()} hackathons: {allScoresSum / allHackathons.Count()}");
        
        Hackathon? first = allHackathons.Where(h => h.Id == 9).FirstOrDefault();
        if (first != null)
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine($"Hackathon: {first.Id}, score: {first.Score}");
            if (first.Juniors == null)
            {
                Console.WriteLine("Juniors are not found");
            } else {
                foreach (var j in first.Juniors)
                {
                    Console.WriteLine($"junior: {j.Id}, name: {j.Name}");
                }
            }
            
            if (first.TeamLeads == null)
            {
                Console.WriteLine("Teamleads are not found");
            } else
            {
                foreach (var t in first.TeamLeads)
                {
                    Console.WriteLine($"teamlead: {t.Id}, name: {t.Name}");
                }
            }

            if (first.Wishlists == null)
            {
                Console.WriteLine("Wishlist are not found");
            } else
            {
                foreach (var w in first.Wishlists)
                {
                    Console.WriteLine($"wishlist: {w.EmployeeId}, desiredEmployees: {w.DesiredEmployees[0]}, {w.DesiredEmployees[1]}, {w.DesiredEmployees[2]}, {w.DesiredEmployees[3]}, {w.DesiredEmployees[4]}");
                }
            }
            
            if (first.Teams == null)
            {
                Console.WriteLine("Teams are not found");
            } else
            {
                foreach (var t in first.Teams)
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