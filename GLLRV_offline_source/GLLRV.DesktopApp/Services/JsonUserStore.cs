using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp.Services
{
    public static class JsonUserStore
    {
        private static string GetFilePath()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(baseDir, "data", "usuarios.json");
        }

        public static List<Usuario> LoadUsers()
        {
            var path = GetFilePath();
            if (!File.Exists(path))
                return new List<Usuario>();

            var json = File.ReadAllText(path);
            var root = JsonSerializer.Deserialize<UsuariosRoot>(json);
            return root?.usuarios ?? new List<Usuario>();
        }

        public static void UpdateUser(Usuario usuario)
        {
            var users = LoadUsers();
            var existing = users.FirstOrDefault(u =>
                u.NomeUsuario.Equals(usuario.NomeUsuario, StringComparison.OrdinalIgnoreCase));

            if (existing != null)
            {
                existing.Senha = usuario.Senha;
                existing.FraseSeguranca = usuario.FraseSeguranca;
                existing.PrimeiroAcesso = usuario.PrimeiroAcesso;
                existing.Nivel = usuario.Nivel;
                existing.Categoria = usuario.Categoria;
                existing.NomeCompleto = usuario.NomeCompleto;
            }
            else
            {
                users.Add(usuario);
            }

            var json = JsonSerializer.Serialize(new UsuariosRoot { usuarios = users }, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(GetFilePath(), json);
        }

        private class UsuariosRoot
        {
            public List<Usuario> usuarios { get; set; } = new();
        }
    }
}
