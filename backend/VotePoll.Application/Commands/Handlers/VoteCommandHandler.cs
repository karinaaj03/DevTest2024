using MediatR;
using VotePoll.Application.Commands.Command;
using VotePoll.Domain.Entities.Concretes;
using VotePoll.Infrastructure.Repositories.Interfaces;

namespace VotePoll.Application.Commands.Handlers;

public class VoteCommandHandler : IRequestHandler<VoteCommand, int>
{
    private readonly IPollRepository _repository;

    public VoteCommandHandler(IPollRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(VoteCommand request, CancellationToken cancellationToken)
    {
        var hasVoted = await _repository.HasUserVoteAsync(request.PollId, request.EmailVoter);
        
        var poll = await _repository.GetByIdAsync(request.PollId);
        
        var options = poll.Options.FirstOrDefault(o => o.Id == request.OptionId);

        options.Votes++;

        var vote = new Vote
        {
            PollId = request.PollId,
            PollOptionId = request.OptionId,
            EmailVoter = request.EmailVoter,
        };

        return 1;
    }
}