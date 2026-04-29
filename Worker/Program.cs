using Core.Services;
using Infra.Enforces;
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

        // CORE
        services.AddSingleton<PolicyEngine>();

        // WORKER
        services.AddHostedService<GuardWorker>();
    })
    .Build()
    .Run();