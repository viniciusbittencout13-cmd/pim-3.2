using System;
using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;
using GLLRV.DesktopApp.Models;
using GLLRV.DesktopApp.Services;
using GLLRV.DesktopApp.Views;
using System.Text.Json;

namespace GLLRV.DesktopApp
{
    public partial class App : Application
    {
        public static IConfiguration Configuration { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);

                var basePath = AppContext.BaseDirectory;

                // appsettings.json simples para apontar a pasta de dados
                var appSettingsPath = Path.Combine(basePath, "appsettings.json");
                if (!File.Exists(appSettingsPath))
                {
                    File.WriteAllText(appSettingsPath, """
{
  "DataFolder": "data"
}
""");
                }

                Configuration = new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                    .Build();

                var dataFolder = Configuration["DataFolder"] ?? "data";
                var fullDataPath = Path.Combine(basePath, dataFolder);
                Directory.CreateDirectory(fullDataPath);

                // Garante arquivo de usuários com Vinicius padrão
                var usuariosPath = Path.Combine(fullDataPath, "usuarios.json");
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
                        PrimeiroAcesso = true
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

                // Abre a tela de login (não entra mais direto)
                var login = new LoginWindow();
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
