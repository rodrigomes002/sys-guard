namespace Infra;

public class HostsFileBlocker
{
    private readonly string hostsPath = 
        @"C:\Windows\System32\drivers\etc\hosts";

    public void BlockSite(string domain)
    {
        var entry = $"127.0.0.1 {domain}";

        if (!File.ReadAllText(hostsPath).Contains(domain))
        {
            File.AppendAllText(hostsPath, Environment.NewLine + entry);
        }
    }
}