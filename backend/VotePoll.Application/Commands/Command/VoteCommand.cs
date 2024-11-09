using MediatR;

namespace VotePoll.Application.Commands.Command;

public class VoteCommand : IRequest<int>
{
    public Guid PollId { get; set; }
    public Guid OptionId { get; set; }
    public string EmailVoter { get; set; }
}