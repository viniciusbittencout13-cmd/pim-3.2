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
        private static string DataFolder =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");

        private static string UsersFile =>
            Path.Combine(DataFolder, "usuarios.json");

        // Carrega usuários do JSON (ou cria usuário padrão)
        public static List<Usuario> Load()
        {
            try
            {
                if (!Directory.Exists(DataFolder))
                    Directory.CreateDirectory(DataFolder);

                if (!File.Exists(UsersFile))
                {
                    var defaultUsers = new List<Usuario>
                    {
                        new Usuario
                        {
                            Username = "vinicius",
                            Senha = "123",
                            Nivel = 2,
                            Atribuicoes = "Servidores e gerenciamento de rede",
                            PrimeiroAcesso = true,
                            FraseSeguranca = ""
                        }
                    };

                    Save(defaultUsers);
                    return defaultUsers;
                }

                var json = File.ReadAllText(UsersFile);
                var users = JsonSerializer.Deserialize<List<Usuario>>(json);
                return users ?? new List<Usuario>();
            }
            catch
            {
                return new List<Usuario>();
            }
        }

        // Alias p/ manter compatibilidade com outros arquivos
        public static List<Usuario> LoadUsers() => Load();
        public static List<Usuario> Loadusers() => Load(); // se em algum lugar estiver com "Loadusers"

        public static void Save(List<Usuario> usuarios)
        {
            if (!Directory.Exists(DataFolder))
                Directory.CreateDirectory(DataFolder);

            var json = JsonSerializer.Serialize(
                usuarios,
                new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(UsersFile, json);
        }

        public static Usuario? Find(string username, string senha)
        {
            var users = Load();

            return users.FirstOrDefault(u =>
                string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase)
                && u.Senha == senha);
        }

        public static Usuario? FindByUsername(string username)
        {
            var users = Load();

            return users.FirstOrDefault(u =>
                string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase));
        }

        // Usado pelo FirstAccess / atualização de dados
        public static void UpdateUser(Usuario user)
        {
            var users = Load();

            var idx = users.FindIndex(u =>
                u.Username.Equals(user.Username, StringComparison.OrdinalIgnoreCase));

            if (idx >= 0)
                users[idx] = user;
            else
                users.Add(user);

            Save(users);
        }
    }
}
