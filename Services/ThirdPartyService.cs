namespace cuna.Services;

using Models;
using Repository;

public class ThirdPartyService : IThirdPartyService
{
    private readonly IThirdPartyStatusRepository _repository;
    private readonly ITransportService _transport;

    public ThirdPartyService(IThirdPartyStatusRepository repository, ITransportService transport)
    {
        _repository = repository;
        _transport = transport;
    }

    public async Task<string> SubmitAsync(string content)
    {
        var id = await _repository.AddAsync(content);
        await _transport.SubmitAsync(id, content);
        return id;
    }

    public Task UpdateStatusAsync(ThirdPartyUpdateStatus status) => _repository.UpdateAsync(status);

    public async Task<ThirdPartyStatus> GetAsync(string id)
    {
        var history = await _repository.GetAsync(id);
        var body = history.FirstOrDefault()?.Detail ?? string.Empty;
        var last = history.Skip(1).LastOrDefault();
        return new ThirdPartyStatus
        {
            Body = body,
            Detail = last?.Detail ?? string.Empty,
            Status = last?.Status ?? string.Empty,
        };
    }

    public async Task<ThirdPartyStatusHistory> GetHistoryAsync(string id)
    {
        var history = await _repository.GetAsync(id);
        var init = history.FirstOrDefault();
        var records = history.Skip(1).ToArray();
        var first = records.FirstOrDefault();
        var last = records.LastOrDefault();
        return new ThirdPartyStatusHistory
        {
            Id = id,
            Created = init?.Timestamp ?? DateTime.MinValue,
            LastUpdated = last?.Timestamp ?? DateTime.MinValue,
            Body = init?.Detail ?? string.Empty,
            Detail = last?.Detail ?? string.Empty,
            Status = last?.Status ?? string.Empty,
            History = records
        };
    }
}