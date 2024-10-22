using MediatR;

using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.HR;
using Nsu.HackathonProblem.Strategies;
using Nsu.HackathonProblem.Utils;
using Nsu.HackathonProblem.DataTransfer;

class Experiment(IMediator mediator)
{
    public async Task Run()
    {
        while (true)
        {
            Console.WriteLine("Введите номер действия:");
            Console.WriteLine("1 - Запустить хакатон");
            Console.WriteLine("2 - Получить средний результат по всем хакатонам");
            Console.WriteLine("3 - Получить хакатон по ID");
            Console.WriteLine("0 - Выход");
            var input = Console.ReadLine();
            if (input == "0") break;

            switch (input)
            {
                case "1":
                    var harmonicMean = await mediator.Send(new RunHackathonRequest());
                    Console.WriteLine($"Среднее гармоническое проведенного хакатона: {harmonicMean}");
                    break;

                case "2":
                    var avgScore = await mediator.Send(new GetAverageScoreRequest());
                    Console.WriteLine($"Средний результат по всем хакатонам: {avgScore}");
                    break;

                case "3":
                    Console.WriteLine("Введите ID хакатона:");
                    int id = int.Parse(Console.ReadLine());
                    var hackathon = await mediator.Send(new GetHackathonByIdRequest(id));
                    if (hackathon != null)
                    {
                        Console.WriteLine("----------------------------------");
                        Console.WriteLine($"Hackathon: {hackathon.Id}, score: {hackathon.Score}");

                        if (hackathon.Wishlists == null)
                        {
                            Console.WriteLine("Wishlist are not found");
                        } else
                        {
                            foreach (var w in hackathon.Wishlists)
                            {
                                Console.WriteLine($"wishlist: {w.Employee}, desiredEmployees: {w.DesiredEmployees[0]}, {w.DesiredEmployees[1]}, {w.DesiredEmployees[2]}, {w.DesiredEmployees[3]}, {w.DesiredEmployees[4]}");
                            }
                        }
                        
                        if (hackathon.Teams == null)
                        {
                            Console.WriteLine("Teams are not found");
                        } else
                        {
                            foreach (var t in hackathon.Teams)
                            {
                                Console.WriteLine($"team: {t.Junior}, {t.TeamLead}");
                            }
                        }
                    } else 
                    {
                        Console.WriteLine("Hackathon is not found");
                    }
                    break;

                default:
                    Console.WriteLine("Неверный ввод. Попробуйте снова.");
                    break;
            }
        }
    }
}