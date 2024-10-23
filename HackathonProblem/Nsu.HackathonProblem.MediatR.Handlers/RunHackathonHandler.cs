using MediatR;
using Microsoft.Extensions.Options;

using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.DataTransfer;
using Nsu.HackathonProblem.HR;
using Nsu.HackathonProblem.Utils;

class RunHackathonHandler : IRequestHandler<RunHackathonRequest, double>
{
    private readonly HRManager manager;
    private readonly HRDirector director;
    private readonly IDataTransfer dataTransfer;
    private readonly HackathonOptions options;

    public RunHackathonHandler(HRManager manager, HRDirector director, IDataTransfer dataTransfer, IOptions<HackathonOptions> hackathonOptions)
    {
        this.manager = manager;
        this.director = director;
        this.dataTransfer = dataTransfer;
        this.options = hackathonOptions.Value;
    }

    public Task<double> Handle(RunHackathonRequest request, CancellationToken cancellationToken)
    {
        // reading juniors
        var juniors = EmployeesReader.ReadJuniors(options.juniorsFile);
        // reading teamLeads
        var teamLeads = EmployeesReader.ReadTeamLeads(options.teamLeadsFile);

        double sumScore = 0.0;

        Hackathon hackathon = new Hackathon();
        double score = hackathon.RunHackathon(manager, director, teamLeads, juniors);
        hackathon.Score = score;
        dataTransfer.SaveHackathon(hackathon);

        return Task.FromResult(score);
    }
}