using System;
using System.IO;
using System.Globalization;
using System.Linq;
using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.HR;
using Nsu.HackathonProblem.Utils;

namespace Nsu.HackathonProblem.HackathonProblem
{
    class Program
    {
        const int HACKATHON_REPEATS = 1;
        static void Main(string[] args)
        {
            // reading juniors
            var juniors = EmployeesReader.ReadEmployees("Juniors5.csv");
            foreach (var junior in juniors)
            {
                Console.WriteLine($"Junior Id: {junior.Id}, Name: {junior.Name}");
            }

            // reading teamLeads
            var teamLeads = EmployeesReader.ReadEmployees("Teamleads5.csv");
            foreach (var teamlead in teamLeads)
            {
                Console.WriteLine($"Teamlead Id: {teamlead.Id}, Name: {teamlead.Name}");
            }

            HRManager manager = new HRManager();
            HRDirector director = new HRDirector();
            Hackathon hackathon = new Hackathon();
            double sumScore = 0.0;
            for (int i = 0; i < HACKATHON_REPEATS; i++)
            {
                double score = hackathon.RunHackathon(manager, director, teamLeads, juniors);
                Console.WriteLine($"score [i={i}]: {score}");
                sumScore += score;
            }

            Console.WriteLine($"Average score: {sumScore / HACKATHON_REPEATS}");
        }
    }
}