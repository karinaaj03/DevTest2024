using MediatR;
using VotePoll.Application.Dtos;

namespace VotePoll.Application.Queries.Query;

public class GetPollsQuery : IRequest<List<PollDto>>
{
    
}