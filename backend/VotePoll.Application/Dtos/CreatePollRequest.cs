namespace VotePoll.Application.Dtos;

public class CreatePollRequest
{
    public string Name { get; set; }
    public List<PollOptionsDto> Options { get; set; }
}