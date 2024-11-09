using VotePoll.Domain.Entities.Concretes;
using VotePoll.Infrastructure.Repositories.Bases;
using VotePoll.Infrastructure.Repositories.Interfaces;

namespace VotePoll.Infrastructure.Repositories.Concretes;

public class PollRepository : BaseRepository<Poll>, IPollRepository
{
    public static readonly List<Vote> Votes = new();
    public readonly IPollOptionRepository _pollOptionRepository;

    public PollRepository(IPollOptionRepository pollOptionRepository)
    {
        _pollOptionRepository = pollOptionRepository;
    }

    public override async Task<int> CreateAsync(Poll entity)
    {
        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }
        foreach (var option in entity.Options)
        {
            if (option.Id == Guid.Empty)
            {
                option.Id = Guid.NewGuid();
            }
            await _pollOptionRepository.CreateAsync(option);
        }
        
        return await base.CreateAsync(entity);
    }
    
    public async Task<bool> HasUserVoteAsync(Guid pollId, string emailVoter)
    {
        return await Task.FromResult(
            Votes.Any(v => v.PollId == pollId && v.EmailVoter == emailVoter)
        );
    }

    public async Task<Poll> GetByIdAsync(Guid pollId)
    {
        return await Task.FromResult(
            Data.FirstOrDefault(p => p.Id == pollId)
        );
    }
}