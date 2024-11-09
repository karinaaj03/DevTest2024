using MediatR;
using VotePoll.Application.Dtos;
using VotePoll.Application.Queries.Query;
using VotePoll.Infrastructure.Repositories.Interfaces;

namespace VotePoll.Application.Queries.Handlers;

public class GetPollsQueryHandler : IRequestHandler<GetPollsQuery, List<PollDto>>
{
    private readonly IPollRepository _pollRepository;
    private readonly IPollOptionRepository _pollOptionRepository;

    public GetPollsQueryHandler(IPollRepository pollRepository, IPollOptionRepository pollOptionRepository)
    {
        _pollRepository = pollRepository;
        _pollOptionRepository = pollOptionRepository;
    }

    public async Task<List<PollDto>> Handle(GetPollsQuery request, CancellationToken cancellationToken)
    {
        var polls = await _pollRepository.GetAllAsync();
        var options = await _pollOptionRepository.GetAllAsync();
        
        if (polls == null)
        {
            return [];
        }
        var pollToShow = new List<PollDto>();
        foreach (var poll in polls)
        {
            pollToShow.Add(new PollDto()
            {
                Id = poll.Id,
                Name = poll.Name,
                Options = options
                    .Where(o => poll.Options.Any(p=> p.Id == o.Id))
                    .Select(o => new PollOptionQueryDto
                    {
                        Id = Guid.NewGuid(),
                        Name = o.Name,
                        Votes = o.Votes
                    }).ToList()
            });
        }

        return pollToShow;
    }
}