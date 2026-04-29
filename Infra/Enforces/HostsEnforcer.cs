using Core;
using Core.Enums;
using Core.Services;

namespace Infra.Enforces;

public class HostsEnforcer : IPolicyEnforcer
{
    public Task EnforceAsync(IEnumerable<Policy> policies)
    {
        var sites = policies
            .Where(p => p.Type == PolicyType.Website && p.Enabled)
            .Select(p => p.Target);

        // chama HostsManager
        return Task.CompletedTask;
    }
}