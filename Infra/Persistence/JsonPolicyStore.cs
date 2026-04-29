using System.Text.Json;
using Core;
using Core.Services;

namespace Infra.Persistence;

public class JsonPolicyStore : IPolicyStore
{
    private readonly string _path = "policies.json";

    public async Task<IEnumerable<Policy>> GetAllAsync()
    {
        if (!File.Exists(_path))
            return new List<Policy>();

        var json = await File.ReadAllTextAsync(_path);
        return JsonSerializer.Deserialize<List<Policy>>(json);
    }

    public async Task SaveAsync(IEnumerable<Policy> policies)
    {
        var json = JsonSerializer.Serialize(policies, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        await File.WriteAllTextAsync(_path, json);
    }
}