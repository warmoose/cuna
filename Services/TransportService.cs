namespace cuna.Services;

using System.Text.Json;

public interface ITransportService
{
    Task SubmitAsync(string id, string content);
}

public class LocalTransportService : ITransportService
{
    public async Task SubmitAsync(string id, string content)
    {
        await Task.CompletedTask;
    }
}

public class RemoteTransportService : ITransportService
{
    public async Task SubmitAsync(string id, string body)
    {
        var obj = new { body = body, callback = $"/callback/{id}" };
        var content = JsonSerializer.Serialize(obj);
        HttpClient client = new();
        await client.PostAsync("https://localhost:5001/request", new StringContent(content));
        await Task.CompletedTask;
    }
}