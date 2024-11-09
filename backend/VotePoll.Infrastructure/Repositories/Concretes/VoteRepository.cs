using VotePoll.Domain.Entities.Concretes;
using VotePoll.Infrastructure.Repositories.Bases;
using VotePoll.Infrastructure.Repositories.Interfaces;

namespace VotePoll.Infrastructure.Repositories.Concretes;

public class VoteRepository : BaseRepository<Vote>, IVoteRepository
{
    
}