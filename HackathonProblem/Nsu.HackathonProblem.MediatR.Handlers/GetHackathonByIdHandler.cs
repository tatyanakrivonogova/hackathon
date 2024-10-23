using MediatR;
using Microsoft.Extensions.Options;

using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.DataTransfer;

class GetHackathonByIdHandler : IRequestHandler<GetHackathonByIdRequest, Hackathon>
{
    private readonly IDataTransfer dataTransfer;
    private readonly HackathonOptions options;

    public GetHackathonByIdHandler(IDataTransfer dataTransfer, IOptions<HackathonOptions> hackathonOptions)
    {
        this.dataTransfer = dataTransfer;
        this.options = hackathonOptions.Value;
    }

    public Task<Hackathon> Handle(GetHackathonByIdRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(dataTransfer.LoadHackathonById(request.Id));
    }
}