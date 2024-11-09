using System.Security.Principal;
using VotePoll.Domain.Entities.Interfaces;
using VotePoll.Infrastructure.Repositories.Interfaces;

namespace VotePoll.Infrastructure.Repositories.Bases;

public class BaseRepository<T> : IRepository<T> where T : class, IEntity, new()
{
    public static List<T> Data = new();
    
    public virtual async Task<int> CreateAsync(T entity)
    {
        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }
        
        Data.Add(entity);
        await Task.CompletedTask;
        return Data.Count;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await Task.FromResult(Data);
    }
}