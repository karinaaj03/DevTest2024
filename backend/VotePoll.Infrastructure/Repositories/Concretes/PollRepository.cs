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

    public async Task<int> CreateAsync(Poll entity)
    {
        var options = entity.Options;
        foreach (var option in options)
        {
            option.Id = Guid.NewGuid();
            _pollOptionRepository.CreateAsync(option);
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