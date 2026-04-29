using Core.Enums;

namespace Core;

public class Policy
{
    public Guid Id { get; set; }
    public PolicyType Type { get; set; }
    public string Target { get; set; } // domínio ou processo
    public bool Enabled { get; set; }
}