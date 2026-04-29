using Core.Services;
using Infra.Enforces;
using Infra.OS;
using Infra.Persistence;
using Worker;

Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices((context, services) =>
    {
        // STORE
        services.AddSingleton<IPolicyStore, JsonPolicyStore>();

        // ENFORCERS
        services.AddSingleton<IPolicyEnforcer, HostsEnforcer>();
        services.AddSingleton<IPolicyEnforcer, ProcessEnforcer>();
        services.AddSingleton<HostsManager>();
        
        // CORE
        services.AddSingleton<PolicyEngine>();

        // WORKER
        services.AddHostedService<GuardWorker>();
    })
    .Build()
    .Run();