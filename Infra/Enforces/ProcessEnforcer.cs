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
            var processes = FindProcesses(app);

            foreach (var p in processes)
                p.Kill();
        }

        return Task.CompletedTask;
    }
    
    private static IEnumerable<Process> FindProcesses(string app)
    {
        var name = app.Replace(".exe", "", StringComparison.OrdinalIgnoreCase);

        return Process.GetProcessesByName(name);
    }
}