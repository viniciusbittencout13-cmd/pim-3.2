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
            InitializeComponent();
            _usuario = usuario;

            // Se tiver um label com o nome do usuário, pode preencher aqui
            // UserNameTextBlock.Text = _usuario.NomeUsuario;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            var frase = SecurityPhraseTextBox.Text.Trim();
            var novaSenha = NewPasswordBox.Password;
            var repetir = ConfirmPasswordBox.Password;

            if (string.IsNullOrWhiteSpace(frase) ||
                string.IsNullOrWhiteSpace(novaSenha) ||
                string.IsNullOrWhiteSpace(repetir))
            {
                MessageBox.Show("Preencha todos os campos.", "Aviso",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (novaSenha != repetir)
            {
                MessageBox.Show("As senhas não conferem.", "Erro",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (novaSenha.Length < 4)
            {
                MessageBox.Show("Defina uma senha com pelo menos 4 caracteres.", "Aviso",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Atualiza dados do usuário
            _usuario.SenhaSha256Hex = Auth.Sha256Hex(novaSenha);
            _usuario.FraseSeguranca = frase;
            _usuario.PrimeiroAcesso = false;

            SalvarUsuario(_usuario);

            MessageBox.Show("Primeiro acesso configurado com sucesso!", "Sucesso",
                MessageBoxButton.OK, MessageBoxImage.Information);

            var main = new MainWindow();
            main.Show();

            // Fecha esta janela e a de login (se ainda existir)
            if (Owner != null)
                Owner.Close();

            Close();
        }

        private void SalvarUsuario(Usuario usuario)
        {
            var dir = App.DataFolderPath;
            Directory.CreateDirectory(dir);

            var path = Path.Combine(dir, "usuarios.json");
            Usuario[] lista;

            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                lista = JsonSerializer.Deserialize<Usuario[]>(json) ?? Array.Empty<Usuario>();
            }
            else
            {
                lista = Array.Empty<Usuario>();
            }

            bool found = false;
            for (int i = 0; i < lista.Length; i++)
            {
                if (lista[i].UsuarioID == usuario.UsuarioID ||
                    lista[i].NomeUsuario.Equals(usuario.NomeUsuario, StringComparison.OrdinalIgnoreCase))
                {
                    lista[i] = usuario;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                lista = lista.Concat(new[] { usuario }).ToArray();
            }

            var novoJson = JsonSerializer.Serialize(
                lista,
                new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            File.WriteAllText(path, novoJson);
        }
    }
}
