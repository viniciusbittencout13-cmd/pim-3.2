using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using GLLRV.DesktopApp.Models;
using GLLRV.DesktopApp.Services;

namespace GLLRV.DesktopApp.Views
{
    public partial class FirstAccessWindow : Window
    {
        private readonly Usuario _usuario;

        public FirstAccessWindow(Usuario usuario)
        {
            _usuario = usuario;
            InitializeComponent();
        }

        private void ConfirmarButton_Click(object sender, RoutedEventArgs e)
        {
            var frase = FraseSegurancaTextBox.Text.Trim();
            var nova = NovaSenhaBox.Password;
            var repete = RepeteSenhaBox.Password;

            if (string.IsNullOrWhiteSpace(frase) ||
                string.IsNullOrWhiteSpace(nova) ||
                string.IsNullOrWhiteSpace(repete))
            {
                MessageBox.Show("Preencha todos os campos.", "Aviso",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (nova != repete)
            {
                MessageBox.Show("As senhas não conferem.", "Erro",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _usuario.FraseSeguranca = frase;
            _usuario.SenhaSha256Hex = Auth.Sha256Hex(nova);
            _usuario.PrimeiroAcesso = false;

            SalvarUsuario(_usuario);

            MessageBox.Show("Dados salvos com sucesso!", "Sucesso",
                MessageBoxButton.OK, MessageBoxImage.Information);

            var main = new MainWindow();
            main.Show();

            Owner?.Close(); // fecha LoginWindow que estava escondida
            Close();
        }

        private void SalvarUsuario(Usuario atualizado)
        {
            var basePath = AppContext.BaseDirectory;
            var dataFolder = App.Configuration["DataFolder"] ?? "data";
            var path = Path.Combine(basePath, dataFolder, "usuarios.json");
            if (!File.Exists(path)) return;

            var json = File.ReadAllText(path);
            var lista = JsonSerializer.Deserialize<Usuario[]>(json) ?? Array.Empty<Usuario>();

            for (int i = 0; i < lista.Length; i++)
            {
                if (lista[i].UsuarioID == atualizado.UsuarioID)
                {
                    lista[i] = atualizado;
                    break;
                }
            }

            var novoJson = JsonSerializer.Serialize(lista,
                new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            File.WriteAllText(path, novoJson);
        }
    }
}
