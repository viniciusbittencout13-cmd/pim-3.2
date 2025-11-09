using System.Windows;
using GLLRV.DesktopApp.Services;
using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp.Views
{
    public partial class LoginWindow : Window
    {
        private readonly Auth _auth = new Auth();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text.Trim();
            var password = PasswordBox.Password;

            var usuario = _auth.Login(username, password);
            if (usuario == null)
            {
                MessageBox.Show("Usuário não encontrado ou senha incorreta.", "Erro",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (usuario.PrimeiroAcesso)
            {
                var first = new FirstAccessWindow(usuario, _auth);
                var result = first.ShowDialog();
                if (result != true)
                    return;
            }

            var main = new MainWindow(usuario);
            main.Show();
            Close();
        }
    }
}
