using System.Text.Json;

namespace BookingApi.Services;

public class EventStore
{
    private readonly string _filePath;

    public EventStore()
    {
        var root = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        var sharedFolder = Path.Combine(root, "shared");
        Directory.CreateDirectory(sharedFolder);

        _filePath = Path.Combine(sharedFolder, "events.jsonl");

        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, string.Empty);
        }
    }

    public async Task AppendAsync<T>(T @event)
    {
        var json = JsonSerializer.Serialize(@event);
        await File.AppendAllTextAsync(_filePath, json + Environment.NewLine);
    }

    public string GetPath() => _filePath;
}