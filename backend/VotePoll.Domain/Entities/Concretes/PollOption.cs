using VotePoll.Domain.Entities.Bases;

namespace VotePoll.Domain.Entities.Concretes;

public class PollOption : BaseEntity
{
    public string Name { get; set; }
    public int Votes { get; set; }
}