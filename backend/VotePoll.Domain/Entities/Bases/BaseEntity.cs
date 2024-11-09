using VotePoll.Domain.Entities.Interfaces;

namespace VotePoll.Domain.Entities.Bases;

public abstract class BaseEntity : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
}