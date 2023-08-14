namespace cuna.Repository;

using Models;
using System.Collections.Concurrent;

public interface IThirdPartyStatusRepository
{
    Task<string> AddAsync(string content);
    Task UpdateAsync(ThirdPartyUpdateStatus status);
    Task<ThirdPartyUpdateStatus[]> GetAsync(string id);
    Task DeleteAsync(string id);
}

public class MemoryRepository : IThirdPartyStatusRepository
{
    private readonly ConcurrentDictionary<string, List<ThirdPartyUpdateStatus>> _status = new();

    public Task<string> AddAsync(string content)
    {
        var status = new ThirdPartyUpdateStatus
        {
            Id = Guid.NewGuid().ToString(),
            Status = ThirdPartyStatus.Initial,
            Detail = content
        };
        _status.TryAdd(status.Id, new List<ThirdPartyUpdateStatus> { status });
        return Task.FromResult(status.Id);
    }

    public Task UpdateAsync(ThirdPartyUpdateStatus status)
    {
        _status[status.Id].Add(status);
        return Task.CompletedTask;
    }

    public Task<ThirdPartyUpdateStatus[]> GetAsync(string id)
    {
        _status.TryGetValue(id, out var status);
        return Task.FromResult(status?.ToArray() ?? Array.Empty<ThirdPartyUpdateStatus>());
    }

    public Task DeleteAsync(string id)
    {
        _status.Remove(id, out _);
        return Task.CompletedTask;
    }
}