using VotePoll.Domain.Entities.Bases;

namespace VotePoll.Domain.Entities.Concretes;

public class Poll : BaseEntity
{
    public string Name { get; set; }
    public List<PollOption> Options { get; set; }
}