namespace VotePoll.Application.Dtos;

public class VoteRequest
{
    public Guid OptionId { get; set; }
    public string EmailVoter { get; set; }
}