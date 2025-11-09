using System.Windows;
using GLLRV.DesktopApp.Services;
using GLLRV.DesktopApp.Models;

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
            var username = UserNameTextBox.Text.Trim(); // nome deve bater com o XAML
            var senha = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(senha))
            {
                MessageBox.Show("Informe usuário e senha.", "Aviso",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var usuario = JsonUserStore.Find(username, senha);

            if (usuario is null)
            {
                MessageBox.Show("Usuário não encontrado.", "Erro",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (usuario.PrimeiroAcesso)
            {
                var first = new FirstAccessWindow(usuario);
                first.Show();
                Close();
                return;
            }

            var main = new MainWindow(usuario);
            main.Show();
            Close();
        }
    }
}
