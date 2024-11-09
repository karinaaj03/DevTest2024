using MediatR;
using VotePoll.Application.Dtos;
using VotePoll.Domain.Entities.Concretes;

namespace VotePoll.Application.Commands.Command;

public class CreatePollCommand : IRequest<PollDto>
{
    public string Name { get; set; }
    public List<PollOptionsDto> Options { get; set; }

    public CreatePollCommand(string name, List<PollOptionsDto> options)
    {
        Name = name;
        Options = options;
    }
}