using Core.Enums;

namespace Core;

public class Policy
{
    public PolicyType Type { get; set; }
    public string Target { get; set; }
    public bool Enabled { get; set; }
}