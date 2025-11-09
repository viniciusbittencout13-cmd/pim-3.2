using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp.Services
{
    public class JsonUserStore
    {
        private readonly string _dataDir;
        private readonly string _usersFile;

        public JsonUserStore()
        {
            _dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "usuarios");
            _usersFile = Path.Combine(_dataDir, "usuarios.json");

            if (!Directory.Exists(_dataDir))
                Directory.CreateDirectory(_dataDir);

            if (!File.Exists(_usersFile))
            {
                // Usuário padrão inicial
                var defaultUser = new Usuario
                {
                    NomeUsuario = "vinicius",
                    NomeCompleto = "Vinicius Bittencourt",
                    Nivel = "2",
                    Categoria = "Servidores e Gerenciamento de Rede",
                    SenhaHash = Sha256("admin"),
                    FraseSeguranca = "",
                    PrimeiroAcesso = true
                };

                SaveUsers(new List<Usuario> { defaultUser });
            }
        }

        public List<Usuario> LoadUsers()
        {
            var json = File.ReadAllText(_usersFile);
            return JsonSerializer.Deserialize<List<Usuario>>(json)
                   ?? new List<Usuario>();
        }

        public void SaveUsers(List<Usuario> users)
        {
            var json = JsonSerializer.Serialize(users,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_usersFile, json);
        }

        public Usuario? GetByUsername(string username)
        {
            return LoadUsers()
                .FirstOrDefault(u =>
                    u.NomeUsuario.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public void UpdateUsuario(Usuario usuario)
        {
            var users = LoadUsers();
            var index = users.FindIndex(u =>
                u.NomeUsuario.Equals(usuario.NomeUsuario, StringComparison.OrdinalIgnoreCase));

            if (index >= 0)
            {
                users[index] = usuario;
                SaveUsers(users);
            }
        }

        public static string Sha256(string text)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(text);
            var hash = sha.ComputeHash(bytes);

            var sb = new StringBuilder();
            foreach (var b in hash)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
}                && u.Senha == hash);
        }

        public static void Update(Usuario user)
        {
            var users = Load();

            var existing = users.FirstOrDefault(u =>
                string.Equals(u.Username, user.Username, StringComparison.OrdinalIgnoreCase));

            if (existing != null)
            {
                existing.Senha = user.Senha;
                existing.Nivel = user.Nivel;
                existing.Atribuicoes = user.Atribuicoes;
                existing.PrimeiroAcesso = user.PrimeiroAcesso;
                existing.FraseSeguranca = user.FraseSeguranca;
            }
            else
            {
                users.Add(user);
            }

            Save(users);
        }

        // Alias para o código que chama UpdateUser
        public static void UpdateUser(Usuario user)
        {
            Update(user);
        }
    }
}
