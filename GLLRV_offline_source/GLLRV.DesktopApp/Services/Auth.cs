using System.Security.Cryptography;
using System.Text;
using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp.Services
{
    public static class Auth
    {
        // Gera SHA256 em hexadecimal
        public static string Sha256Hex(string raw)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(raw ?? string.Empty);
            var hash = sha.ComputeHash(bytes);
            var sb = new StringBuilder(hash.Length * 2);

            foreach (var b in hash)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }

        // Faz o login usando JSON
        public static Usuario? Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            return JsonUserStore.Find(username.Trim(), password);
        }
    }
}
