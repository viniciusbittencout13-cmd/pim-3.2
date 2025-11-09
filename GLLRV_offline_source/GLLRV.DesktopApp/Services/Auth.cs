using System.Security.Cryptography;
using System.Text;
using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp.Services
{
    public static class Auth
    {
        public static string Sha256Hex(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha.ComputeHash(bytes);

            var sb = new StringBuilder(hash.Length * 2);
            foreach (var b in hash)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }

        public static Usuario? Login(string username, string passwordPlain)
        {
            // Autentica usando JSON
            return JsonUserStore.Find(username, passwordPlain);
        }
    }
}
