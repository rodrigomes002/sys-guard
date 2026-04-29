using System.Text.Json;
using Core;
using Core.Defaults;
using Core.Security;
using Core.Services;

namespace Infra.Persistence;

public class JsonPolicyStore : IPolicyStore
{
    private readonly string _path;

    public JsonPolicyStore()
    {
        var folder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
            "SysGuard"
        );

        Directory.CreateDirectory(folder);

        _path = Path.Combine(folder, "policies.json");
    }

    public async Task SaveAsync(IEnumerable<Policy> policies)
    {
        var json = JsonSerializer.Serialize(policies, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        await File.WriteAllTextAsync(_path, json);
    }

    public async Task<IEnumerable<Policy>> GetAllAsync()
    {
        if (!File.Exists(_path))
        {
            return await RestoreDefaultAsync();
        }

        var json = await File.ReadAllTextAsync(_path);

        var currentHash = PolicyIntegrity.ComputeHash(json);
        var defaultHash = GetDefaultHash();

        if (currentHash != defaultHash)
        {
            return await RestoreDefaultAsync();
        }

        return JsonSerializer.Deserialize<List<Policy>>(json)
               ?? new List<Policy>();
    }

    private async Task<IEnumerable<Policy>> RestoreDefaultAsync()
    {
        var policies = DefaultPolicies.Get();

        var json = JsonSerializer.Serialize(policies, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        await File.WriteAllTextAsync(_path, json);

        return policies;
    }

    private string GetDefaultHash()
    {
        var json = JsonSerializer.Serialize(DefaultPolicies.Get());
        return PolicyIntegrity.ComputeHash(json);
    }
}