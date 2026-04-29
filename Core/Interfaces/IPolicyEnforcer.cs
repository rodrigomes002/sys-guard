namespace Core.Services;

public interface IPolicyEnforcer
{
    Task EnforceAsync(IEnumerable<Policy> policies);
}