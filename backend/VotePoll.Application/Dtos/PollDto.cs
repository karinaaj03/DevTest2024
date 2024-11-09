namespace VotePoll.Application.Dtos;

public class PollDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<PollOptionQueryDto> Options { get; set; }
}