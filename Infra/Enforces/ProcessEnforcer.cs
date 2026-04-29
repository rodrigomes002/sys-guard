using System.Diagnostics;
using Core;
using Core.Enums;
using Core.Services;

namespace Infra.Enforces;

public class ProcessEnforcer : IPolicyEnforcer
{
    public Task EnforceAsync(IEnumerable<Policy> policies)
    {
        var apps = policies
            .Where(p => p.Type == PolicyType.Application && p.Enabled)
            .Select(p => p.Target);

        foreach (var app in apps)
        {
            var processes = Process.GetProcessesByName(app);

            foreach (var p in processes)
                p.Kill();
        }

        return Task.CompletedTask;
    }
}