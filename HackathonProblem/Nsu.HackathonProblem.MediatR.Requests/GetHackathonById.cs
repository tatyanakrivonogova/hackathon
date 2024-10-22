using MediatR;

using Nsu.HackathonProblem.Contracts;

class GetHackathonByIdRequest : IRequest<Hackathon>
{
    public int Id { get; }

    public GetHackathonByIdRequest(int id)
    {
        Id = id;
    }
}