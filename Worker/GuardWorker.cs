using System.Runtime.InteropServices.JavaScript;
using Core.Services;

namespace Worker;

public class GuardWorker : BackgroundService
{
    private readonly PolicyEngine _engine;
    private readonly IPolicyStore _store;
    private readonly ILogger<GuardWorker> _logger;

    public GuardWorker(PolicyEngine engine, IPolicyStore store,  ILogger<GuardWorker> logger)
    {
        _engine = engine;
        _store = store;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            var policies = await _store.GetAllAsync();
            
            _logger.LogInformation("Processing policies: {time}", DateTimeOffset.Now);
            await _engine.ApplyAsync(policies);
            
            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
        }
    }
}