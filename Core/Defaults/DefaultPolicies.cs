using Core.Enums;

namespace Core.Defaults;

public static class DefaultPolicies
{
    public static List<Policy> Get()
    {
        return new List<Policy>
        {
            // 🌐 Websites
            new Policy
            {
                Type = PolicyType.Website,
                Target = "facebook.com",
                Enabled = true
            },
            new Policy
            {
                Type = PolicyType.Website,
                Target = "piratebayproxy.net",
                Enabled = true
            },
            new Policy
            {
                Type = PolicyType.Website,
                Target = "thepiratebay3.co",
                Enabled = true
            },
            new Policy
            {
                Type = PolicyType.Website,
                Target = "www2.thepiratebay3.co",
                Enabled = true
            },
            new Policy
            {
                Type = PolicyType.Website,
                Target = "utorrent.com",
                Enabled = true
            },
            new Policy
            {
                Type = PolicyType.Website,
                Target = "lite.utorrent.com",
                Enabled = true
            },
            new Policy
            {
                Type = PolicyType.Website,
                Target = "1377x.is",
                Enabled = true
            },

            // 💻 Applications
            new Policy
            {
                Type = PolicyType.Application,
                Target = "utweb.exe",
                Enabled = true
            },
            new Policy
            {
                Type = PolicyType.Application,
                Target = "BitTorrent.exe",
                Enabled = true
            },
            new Policy
            {
                Type = PolicyType.Application,
                Target = "qbittorrent.exe",
                Enabled = true
            },
            new Policy
            {
                Type = PolicyType.Application,
                Target = "uTorrent.exe",
                Enabled = true
            }
        };
    }
}