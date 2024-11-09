using VotePoll.Domain.Entities.Concretes;

namespace VotePoll.Infrastructure.Repositories.Interfaces;

public interface IPollRepository : IRepository<Poll>
{
    Task<bool> HasUserVoteAsync(Guid pollId, string emailVoter);
    Task<Poll> GetByIdAsync(Guid pollId);
}