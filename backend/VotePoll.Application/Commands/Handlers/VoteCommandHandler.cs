using System.ComponentModel.DataAnnotations;
using MediatR;
using VotePoll.Application.Commands.Command;
using VotePoll.Domain.Entities.Concretes;
using VotePoll.Infrastructure.Repositories.Concretes;
using VotePoll.Infrastructure.Repositories.Interfaces;

namespace VotePoll.Application.Commands.Handlers;

public class VoteCommandHandler : IRequestHandler<VoteCommand, int>
{
    private readonly IPollRepository _pollRepository;
    private readonly IPollOptionRepository _pollOptionRepository;
    private readonly IVoteRepository _voteRepository;

    public VoteCommandHandler(
        IPollRepository pollRepository,
        IPollOptionRepository pollOptionRepository,
        IVoteRepository voteRepository)
    {
        _pollRepository = pollRepository;
        _pollOptionRepository = pollOptionRepository;
        _voteRepository = voteRepository;
    }

    public async Task<int> Handle(VoteCommand request, CancellationToken cancellationToken)
    {
        if (!IsValidEmail(request.EmailVoter))
            throw new ValidationException("Invalid email address");

        var poll = await _pollRepository.GetByIdAsync(request.PollId);
        if (poll == null)
            throw new ValidationException("Poll not found");

        var option = poll.Options.FirstOrDefault(o => o.Id == request.OptionId);
        if (option == null)
            throw new ValidationException("Option not found");

        var hasVoted = await _pollRepository.HasUserVoteAsync(request.PollId, request.EmailVoter);
        if (hasVoted)
            throw new ValidationException("User has already voted in this poll");

        option.Votes++;

        var vote = new Vote
        {
            PollId = request.PollId,
            PollOptionId = request.OptionId,
            EmailVoter = request.EmailVoter,
        };

        var voteId = await _voteRepository.CreateAsync(vote);
        
        PollRepository.Votes.Add(vote);

        return voteId;
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}