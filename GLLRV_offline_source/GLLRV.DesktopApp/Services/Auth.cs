using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp.Services
{
    public static class Auth
    {
        public static string UsuariosPath =>
            Path.Combine(App.DataFolderPath, "usuarios.json");

        public static List<Usuario> CarregarUsuarios()
        {
            if (!File.Exists(UsuariosPath))
            {
                Directory.CreateDirectory(App.DataFolderPath);

                var admin = new Usuario
                {
                    UsuarioID = 1,
                    Nome = "Vinicius Bittencourt",
                    NomeUsuario = "vinicius",
                    Senha = "admin",
                    EhTecnico = true,
                    NivelTecnico = 2,
                    NivelPermissao = 2,
                    Categoria = "Servidores e Gerenciamento de Rede",
                    Email = "vinicius@gllrv.local",
                    Telefone = "(11) 99999-0001",
                    PrimeiroAcesso = true,
                    FraseSeguranca = ""
                };

                var lista = new List<Usuario> { admin };

                var jsonNovo = JsonSerializer.Serialize(
                    lista,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                File.WriteAllText(UsuariosPath, jsonNovo);
                return lista;
            }

            var conteudo = File.ReadAllText(UsuariosPath);

            var usuarios = JsonSerializer.Deserialize<List<Usuario>>(
                               conteudo,
                               new JsonSerializerOptions
                               {
                                   PropertyNameCaseInsensitive = true
                               })
                           ?? new List<Usuario>();

            return usuarios;
        }

        public static void SalvarUsuarios(List<Usuario> usuarios)
        {
            Directory.CreateDirectory(App.DataFolderPath);

            var json = JsonSerializer.Serialize(
                usuarios,
                new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            File.WriteAllText(UsuariosPath, json);
        }

        public static Usuario? Login(string nomeUsuario, string senha)
        {
            var usuarios = CarregarUsuarios();

            return usuarios.FirstOrDefault(u =>
                u.NomeUsuario.Equals(nomeUsuario, StringComparison.OrdinalIgnoreCase)
                && u.Senha == senha);
        }
    }
}
