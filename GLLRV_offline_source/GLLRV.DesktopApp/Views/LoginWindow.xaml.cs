using System.Windows;
using GLLRV.DesktopApp.Services;
using GLLRV.DesktopApp.Views; // se MainWindow e FirstAccessWindow estiverem aqui
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
                MessageBox.Show("Usuário não encontrado ou senha incorreta.",
                    "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Se for primeiro acesso, abre tela específica
            if (user.PrimeiroAcesso)
            {
                // Se sua FirstAccessWindow tiver construtor sem parâmetros, use assim:
                var first = new FirstAccessWindow();
                first.Show();
            }
            else
            {
                // Ajuste aqui se seu MainWindow espera o usuário no construtor
                var main = new MainWindow();
                main.Show();
            }

            Close();
        }
    }
}
