using MediatR;
using VotePoll.Application.Commands.Command;
using VotePoll.Application.Dtos;
using VotePoll.Domain.Entities.Concretes;
using VotePoll.Infrastructure.Repositories.Interfaces;

namespace VotePoll.Application.Commands.Handlers;

public class CreatePollHandler : IRequestHandler<CreatePollCommand, PollDto>
{
    private readonly IPollRepository _pollRepository;

    public CreatePollHandler(IPollRepository pollRepository)
    {
        _pollRepository = pollRepository;
    }
    
    public async Task<PollDto> Handle(CreatePollCommand request, CancellationToken cancellationToken)
    {
        var poll = new Poll
        {
            Name = request.Name,
            Options = request.Options.Select((opt) => new PollOption
            {
                Id = Guid.NewGuid(),
                Name = opt.Name,
                Votes = 0
            }).ToList()
        };
        await _pollRepository.CreateAsync(poll);
        return new PollDto
        {
            Id = poll.Id,
            Name = poll.Name,
            Options = poll.Options.Select(o => new PollOptionQueryDto()
            {
                Name = o.Name,
            }).ToList()
        };
    }
}