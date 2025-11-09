using System.Windows;
using GLLRV.DesktopApp.Services;
using GLLRV.DesktopApp.Views;

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
            var senha = PasswordBox.Password.Trim();

            var usuario = Auth.Login(username, senha);

            if (usuario == null)
            {
                MessageBox.Show("Usuário não encontrado.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (usuario.PrimeiroAcesso)
            {
                var first = new FirstAccessWindow(usuario);
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
