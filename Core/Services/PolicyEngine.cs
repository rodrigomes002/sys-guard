namespace Core.Services;

public class PolicyEngine
{
    private readonly IEnumerable<IPolicyEnforcer> _enforcers;

    public PolicyEngine(IEnumerable<IPolicyEnforcer> enforcers)
    {
        _enforcers = enforcers;
    }

    public async Task ApplyAsync(IEnumerable<Policy> policies)
    {
        foreach (var enforcer in _enforcers)
        {
            await enforcer.EnforceAsync(policies);
        }
    }
}