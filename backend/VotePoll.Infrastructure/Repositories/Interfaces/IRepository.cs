namespace VotePoll.Infrastructure.Repositories.Interfaces;

public interface IRepository<T>  where T : class
{
    public Task<int> CreateAsync(T entity);
    public Task<List<T>> GetAllAsync();
}