using Core.Enums;

namespace Core.Mappings;

public static class PolicyMapper
{
    public static IEnumerable<Policy> Map(
        IEnumerable<string> websites,
        IEnumerable<string> apps)
    {
        var sitePolicies = websites.Select(x => new Policy
        {
            Id = Guid.NewGuid(),
            Target = x,
            Type = PolicyType.Website,
            Enabled = true
        });

        var appPolicies = apps.Select(x => new Policy
        {
            Id = Guid.NewGuid(),
            Target = x,
            Type = PolicyType.Application,
            Enabled = true
        });

        return sitePolicies.Concat(appPolicies);
    }
}