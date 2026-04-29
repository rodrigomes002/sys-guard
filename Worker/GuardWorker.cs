using Core.Services;

namespace Worker;

public class GuardWorker : BackgroundService
{
    private readonly PolicyEngine _engine;
    private readonly IPolicyStore _store;

    public GuardWorker(PolicyEngine engine, IPolicyStore store)
    {
        _engine = engine;
        _store = store;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var policies = await _store.GetAllAsync();

            await _engine.ApplyAsync(policies);

            await Task.Delay(5000, stoppingToken);
        }
    }
}