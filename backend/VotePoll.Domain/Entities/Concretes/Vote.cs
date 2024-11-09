using VotePoll.Domain.Entities.Bases;

namespace VotePoll.Domain.Entities.Concretes;

public class Vote : BaseEntity
{
    public Guid PollId { get; set; }
    public Guid PollOptionId { get; set; }
    public string EmailVoter { get; set; }
}