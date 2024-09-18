using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.HR;
using Nsu.HackathonProblem.Utils;

namespace Nsu.HackathonProblem.HackathonProblem
{
    class Program
    {
        const int hackathonRepeats = 10;
        const int teamsCount = 5;
        static void Main(string[] args)
        {
            // reading juniors
            var juniors = EmployeesReader.ReadEmployees("Juniors5.csv");

            // reading teamLeads
            var teamLeads = EmployeesReader.ReadEmployees("Teamleads5.csv");

            HRManager manager = new HRManager();
            HRDirector director = new HRDirector();
            Hackathon hackathon = new Hackathon();
            double sumScore = 0.0;
            for (int i = 0; i < hackathonRepeats; i++)
            {
                double score = hackathon.RunHackathon(manager, director, teamLeads, juniors);
                Console.WriteLine($"score [i={i}]: {score}");
                sumScore += score;
            }

            Console.WriteLine($"Average score: {sumScore / hackathonRepeats}");
        }
    }
}