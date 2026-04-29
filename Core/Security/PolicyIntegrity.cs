using System.Security.Cryptography;
using System.Text;

namespace Core.Security;

public static class PolicyIntegrity
{
    public static string ComputeHash(string content)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(content);
        return Convert.ToBase64String(sha.ComputeHash(bytes));
    }
}