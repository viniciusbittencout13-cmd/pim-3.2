using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp.Services
{
    public static class JsonUserStore
    {
        private static readonly string BaseDir =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");

        private static readonly string UsersDir =
            Path.Combine(BaseDir, "usuarios");

        private static readonly string UsersFile =
            Path.Combine(UsersDir, "usuarios.json");

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true
        };

        // Gera o usuário padrão se não existir
        public static void EnsureSeedUser()
        {
            Directory.CreateDirectory(UsersDir);

            if (!File.Exists(UsersFile) || new FileInfo(UsersFile).Length == 0)
            {
                var admin = new Usuario
                {
                    // IMPORTANTE: esse campo TEM que bater com o que o login usa
                    Username = "vinicius",
                    Senha = Auth.Sha256Hex("admin"),
                    Nivel = 2,
                    Atribuicoes = "Servidores e gerenciamento de rede",
                    PrimeiroAcesso = true,
                    FraseSeguranca = ""
                };

                var list = new List<Usuario> { admin };
                var json = JsonSerializer.Serialize(list, JsonOptions);
                File.WriteAllText(UsersFile, json);
            }
        }

        public static List<Usuario> Load()
        {
            EnsureSeedUser();

            var json = File.ReadAllText(UsersFile);
            var users = JsonSerializer.Deserialize<List<Usuario>>(json, JsonOptions)
                        ?? new List<Usuario>();

            return users;
        }

        public static void Save(List<Usuario> users)
        {
            Directory.CreateDirectory(UsersDir);

            var json = JsonSerializer.Serialize(users, JsonOptions);
            File.WriteAllText(UsersFile, json);
        }

        public static Usuario? Find(string username, string passwordPlain)
        {
            var users = Load();
            var hash = Auth.Sha256Hex(passwordPlain);

            return users.FirstOrDefault(u =>
                string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase)
                && u.Senha == hash);
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
