using System;
using System.IO;
using System.Text.Json;
using System.Windows;
using GLLRV.DesktopApp.Models;
using GLLRV.DesktopApp.Services;
using GLLRV.DesktopApp.Views;

namespace GLLRV.DesktopApp
{
    public partial class App : Application
    {
        // Caminho absoluto da pasta de dados ao lado do .exe
        public static string DataFolderPath { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);

                var basePath = AppContext.BaseDirectory;

                DataFolderPath = Path.Combine(basePath, "data");
                Directory.CreateDirectory(DataFolderPath);

                // Garante usuarios.json com usuário padrão Vinicius se não existir
                var usuariosPath = Path.Combine(DataFolderPath, "usuarios.json");
                if (!File.Exists(usuariosPath))
                {
                    var vinicius = new Usuario
                    {
                        UsuarioID = 1,
                        Nome = "Vinicius Bittencourt",
                        NomeUsuario = "vinicius",
                        SenhaSha256Hex = Auth.Sha256Hex("admin"), // senha inicial
                        EhTecnico = true,
                        NivelTecnico = 2,
                        NivelPermissao = 2,
                        Email = "vinicius@gllrv.local",
                        Telefone = "(11) 99999-0001",
                        PrimeiroAcesso = true,
                        FraseSeguranca = ""
                    };

                    var json = JsonSerializer.Serialize(
                        new[] { vinicius },
                        new JsonSerializerOptions
                        {
                            WriteIndented = true,
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });

                    File.WriteAllText(usuariosPath, json);
                }

                // Abre a tela de login
                var login = new Views.LoginWindow();
                MainWindow = login;
                login.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro ao iniciar a aplicação:\n" + ex,
                    "GLLRV - Erro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                Shutdown();
            }
        }
    }
}
