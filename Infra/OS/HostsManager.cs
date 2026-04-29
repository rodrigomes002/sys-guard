namespace Infra.OS;

using System.Text;

public class HostsManager
{
    private const string StartTag = "# SYS_GUARD_START";
    private const string EndTag = "# SYS_GUARD_END";

    private readonly string _hostsPath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System),
            @"drivers\etc\hosts");

    public async Task ApplyAsync(IEnumerable<string> domains)
    {
        if (!File.Exists(_hostsPath))
            throw new FileNotFoundException("Hosts file não encontrado");

        var content = await File.ReadAllTextAsync(_hostsPath);

        // Remove bloco anterior
        content = RemoveManagedBlock(content);

        // Cria novo bloco
        var block = BuildBlock(domains);

        // Adiciona ao final
        content += Environment.NewLine + block;

        await File.WriteAllTextAsync(_hostsPath, content, Encoding.UTF8);
    }

    private string RemoveManagedBlock(string content)
    {
        var start = content.IndexOf(StartTag);
        var end = content.IndexOf(EndTag);

        if (start >= 0 && end > start)
        {
            var length = (end + EndTag.Length) - start;
            content = content.Remove(start, length);
        }

        return content.Trim();
    }

    private string BuildBlock(IEnumerable<string> domains)
    {
        var sb = new StringBuilder();

        sb.AppendLine(StartTag);

        foreach (var domain in domains.Distinct())
        {
            sb.AppendLine($"127.0.0.1 {domain}");
            sb.AppendLine($"127.0.0.1 www.{domain}");
        }

        sb.AppendLine(EndTag);

        return sb.ToString();
    }
}