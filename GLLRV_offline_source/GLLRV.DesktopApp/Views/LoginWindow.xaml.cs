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
            var username = UserNameTextBox.Text;
            var password = PasswordBox.Password;

            var user = Auth.Login(username, password);

            if (user == null)
            {
                MessageBox.Show(
                    "Usuário não encontrado ou senha incorreta.",
                    "Erro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            if (user.PrimeiroAcesso)
            {
                var first = new FirstAccessWindow(user);
                first.Show();
            }
            else
            {
                var main = new MainWindow();
                main.Show();
            }

            Close();
        }
    }
}
