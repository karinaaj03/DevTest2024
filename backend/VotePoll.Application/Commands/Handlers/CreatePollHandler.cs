using MediatR;
using VotePoll.Application.Commands.Command;
using VotePoll.Application.Dtos;
using VotePoll.Domain.Entities.Concretes;
using VotePoll.Infrastructure.Repositories.Interfaces;

namespace VotePoll.Application.Commands.Handlers;

public class CreatePollHandler : IRequestHandler<CreatePollCommand, PollDto>
{
    private readonly IPollRepository _pollRepository;
    private readonly IPollOptionRepository _pollOptionRepository;

    public CreatePollHandler(
        IPollRepository pollRepository,
        IPollOptionRepository pollOptionRepository)
    {
        _pollRepository = pollRepository;
        _pollOptionRepository = pollOptionRepository;
    }
    
    public async Task<PollDto> Handle(CreatePollCommand request, CancellationToken cancellationToken)
    {
        var options = request.Options.Select(opt => new PollOption
        {
            Name = opt.Name,
            Votes = 0
        }).ToList();

        var poll = new Poll
        {
            Name = request.Name,
            Options = options
        };

        await _pollRepository.CreateAsync(poll);

        return new PollDto
        {
            Id = poll.Id,
            Name = poll.Name,
            Options = poll.Options.Select(o => new PollOptionQueryDto
            {
                Id = o.Id,
                Name = o.Name,
                Votes = o.Votes
            }).ToList()
        };
    }
}