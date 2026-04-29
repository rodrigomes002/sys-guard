using System.Diagnostics;
using Core;
using Core.Enums;
using Core.Services;
using Infra.OS;

namespace Infra.Enforces;

public class HostsEnforcer : IPolicyEnforcer
{
    private readonly HostsManager _hostsManager;
    
    public HostsEnforcer(HostsManager hostsManager)
    {
        _hostsManager = hostsManager;
    }
    
    public async Task EnforceAsync(IEnumerable<Policy> policies)
    {
        var sites = policies
            .Where(p => p.Type == PolicyType.Website && p.Enabled)
            .Select(p => p.Target);

        await _hostsManager.ApplyAsync(sites);
    }
}