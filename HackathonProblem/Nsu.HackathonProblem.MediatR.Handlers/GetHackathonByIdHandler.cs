using MediatR;
using Microsoft.Extensions.Options;

using Nsu.HackathonProblem.Contracts;
using Nsu.HackathonProblem.DataTransfer;

class GetHackathonByIdHandler : IRequestHandler<GetHackathonByIdRequest, Hackathon>
{
    private readonly IDataTransfer dataTransfer;

    public GetHackathonByIdHandler(IDataTransfer dataTransfer, IOptions<HackathonOptions> hackathonOptions)
    {
        this.dataTransfer = dataTransfer;
    }

    public Task<Hackathon> Handle(GetHackathonByIdRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(dataTransfer.LoadHackathonById(request.Id));
    }
}