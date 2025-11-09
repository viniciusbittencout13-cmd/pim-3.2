using System;
using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;
using GLLRV.DesktopApp.Services;
using GLLRV.DesktopApp.Models;
using GLLRV.DesktopApp.Views;

namespace GLLRV.DesktopApp
{
    public partial class App : Application
    {
        public static IConfiguration Configuration { get; private set; } = null!;
        public static IDataStore DataStore { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);

                var basePath = AppContext.BaseDirectory;

                // Garante appsettings.json básico
                var appSettingsPath = Path.Combine(basePath, "appsettings.json");
                if (!File.Exists(appSettingsPath))
                {
                    File.WriteAllText(appSettingsPath, """
{
  "Mode": "Offline",
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

                // Cria usuário padrão: VINICIUS BITTENCOURT (nível 2, Servidores / Rede)
                var usuariosPath = Path.Combine(fullDataPath, "usuarios.json");
                if (!File.Exists(usuariosPath))
                {
                    var vinicius = new Usuario
{
    UsuarioID = 1,
    Nome = "Vinicius Bittencourt",
    NomeUsuario = "vinicius",
    // senha padrão: admin
    SenhaSha256Hex = Auth.Sha256Hex("admin"),
    EhTecnico = true,
    NivelTecnico = 2,
    NivelPermissao = 2,
    Email = "vinicius@gllrv.local",
    Telefone = "(11) 99999-0001"
};


                    var json = System.Text.Json.JsonSerializer.Serialize(
                        new[] { vinicius },
                        new System.Text.Json.JsonSerializerOptions
                        {
                            WriteIndented = true,
                            PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
                        });

                    File.WriteAllText(usuariosPath, json);
                }

                // DataStore JSON offline
                DataStore = new JsonDataStore(fullDataPath);

                // Abre janela principal (já começa em Chamados Pendentes pelo MainWindow)
                var main = new MainWindow();
                MainWindow = main;
                main.Show();
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
