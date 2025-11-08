
using System.Security.Cryptography;
using System.Text;
using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp.Services;

public static class Auth
{
    public static string Sha256Hex(string input)
    {
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(hash);
    }

    public static bool VerifyPasswordHex(string plain, string expectedHex)
        => string.Equals(Sha256Hex(plain), expectedHex, StringComparison.OrdinalIgnoreCase);
}

public sealed class Session
{
    private static Session? _current;
    public static Session Current => _current ??= new Session();
    public Usuario? Usuario { get; set; }
}
