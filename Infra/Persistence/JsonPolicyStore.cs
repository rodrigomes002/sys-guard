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
}