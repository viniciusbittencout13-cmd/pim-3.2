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
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text.Trim();
            var password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Informe usuário e senha.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var usuario = Auth.Login(username, password);

            if (usuario == null)
            {
                MessageBox.Show("Usuário não encontrado.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Primeiro acesso -> trocar senha + frase
            if (usuario.PrimeiroAcesso)
            {
                var first = new FirstAccessWindow(usuario);
                first.Show();
            }
            else
            {
                var main = new MainWindow(usuario);
                main.Show();
            }

            Close();
        }
    }
}
