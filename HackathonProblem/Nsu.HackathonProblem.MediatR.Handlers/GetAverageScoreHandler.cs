using MediatR;
using Microsoft.Extensions.Options;

using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.DataTransfer;

class GetAverageScoreHandler : IRequestHandler<GetAverageScoreRequest, double>
{
    private readonly IDataTransfer dataTransfer;
    private readonly HackathonOptions options;

    public GetAverageScoreHandler(IDataTransfer dataTransfer, IOptions<HackathonOptions> hackathonOptions)
    {
        this.dataTransfer = dataTransfer;
        this.options = hackathonOptions.Value;
    }

    public Task<double> Handle(GetAverageScoreRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(dataTransfer.LoadAllHackathons(options).Average(h => h.Score));
    }
}