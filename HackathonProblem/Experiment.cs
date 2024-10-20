using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Mapster;

using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.HR;
using Nsu.HackathonProblem.Strategies;
using Nsu.HackathonProblem.Utils;
using Nsu.HackathonProblem.Dto;
using Nsu.HackathonProblem.Mapper;

class Experiment(HRDirector director, HRManager manager, 
                 IOptions<HackathonOptions> hackathonOptions)
{
    public void Run()
    {
        HackathonOptions options = hackathonOptions.Value;

        // reading juniors
        var juniors = EmployeesReader.ReadJuniors(options.juniorsFile);
        // reading teamLeads
        var teamLeads = EmployeesReader.ReadTeamLeads(options.teamLeadsFile);

        double sumScore = 0.0;
        using (HackathonContext context = new HackathonContext(options.database))
        {
            context.Employee.AddRange(juniors.Select(junior => junior.Adapt<EmployeeDto>()).ToList());
            context.Employee.AddRange(teamLeads.Select(teamLead => teamLead.Adapt<EmployeeDto>()).ToList());
            context.SaveChanges();

            Hackathon hackathon = new Hackathon();
            for (int i = 0; i < options.hackathonRepeats; i++)
            {
                double score = hackathon.RunHackathon(manager, director, teamLeads, juniors);
                hackathon.Score = score;
                Console.WriteLine($"score [i={i}]: {score}");
                sumScore += score;
                context.Hackathon.Add(hackathon.Adapt<HackathonDto>());
                context.SaveChanges();
            }
        }

        Console.WriteLine($"Average score for {options.hackathonRepeats} hackathons: {sumScore / options.hackathonRepeats}");

        using (HackathonContext context = new HackathonContext(options.database))
        {
            List<Hackathon> allHackathons = context.Hackathon.Select(hackathonDto => hackathonDto.Adapt<Hackathon>()).ToList();
            double allScoresSum = 0.0;
            foreach (Hackathon hackathon in allHackathons)
            {
                allScoresSum += hackathon.Score;
            }
            Console.WriteLine($"Average score for all {allHackathons.Count()} hackathons: {allScoresSum / allHackathons.Count()}");
        }

        int selectedId = 9; // for example
        using (HackathonContext context = new HackathonContext(options.database))
        {
            // var employees = context.Employee;
            Hackathon? selected = context.Hackathon
                                         .Include("Wishlists.Employee")
                                         .Include("Teams.Junior")
                                         .Include("Teams.TeamLead")
                                         .Select(hackathonDto => hackathonDto.Adapt<Hackathon>())
                                         .ToList().Where(h => h.Id == selectedId).FirstOrDefault();
            if (selected != null)
            {
                Console.WriteLine("----------------------------------");
                Console.WriteLine($"Hackathon: {selected.Id}, score: {selected.Score}");
                // if (employees == null)
                // {
                //     Console.WriteLine("Employees are not found");
                // } else {
                //     foreach (var e in employees)
                //     {
                //         Console.WriteLine($"employee: {e.Id}, name: {e.Name}, role: {e.Role}");
                //     }
                // }

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
}