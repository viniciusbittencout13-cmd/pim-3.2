using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using GLLRV.DesktopApp.Models;
using GLLRV.DesktopApp.Services;

namespace GLLRV.DesktopApp.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UserNameTextBox.Text.Trim();
            var password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Informe usuário e senha.", "Aviso",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var usuarios = CarregarUsuarios();
            var usuario = usuarios.FirstOrDefault(u =>
                u.NomeUsuario.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (usuario == null)
            {
                MessageBox.Show("Usuário não encontrado.", "Erro",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var hashDigitado = Auth.Sha256Hex(password);

            if (usuario.PrimeiroAcesso)
            {
                // Primeiro acesso: precisa bater a senha inicial (admin) ou a já cadastrada
                if (hashDigitado != usuario.SenhaSha256Hex)
                {
                    MessageBox.Show("Senha incorreta.", "Erro",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var first = new FirstAccessWindow(usuario);
                first.Owner = this;
                Hide();
                first.Show();
                return;
            }

            // Acesso normal
            if (hashDigitado != usuario.SenhaSha256Hex)
            {
                MessageBox.Show("Usuário ou senha inválidos.", "Erro",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var main = new MainWindow();
            main.Show();
            Close();
        }

        private Usuario[] CarregarUsuarios()
        {
            var basePath = AppContext.BaseDirectory;
            var dataFolder = App.Configuration["DataFolder"] ?? "data";
            var path = Path.Combine(basePath, dataFolder, "usuarios.json");

            if (!File.Exists(path))
                return Array.Empty<Usuario>();

            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Usuario[]>(json) ?? Array.Empty<Usuario>();
        }
    }
}
