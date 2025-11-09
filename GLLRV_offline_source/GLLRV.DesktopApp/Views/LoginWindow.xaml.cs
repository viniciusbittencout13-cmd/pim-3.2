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
            var username = UserNameTextBox.Text.Trim();
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

            // Por enquanto: se for primeiro acesso, podemos só marcar como já acessado e seguir.
            if (usuario.PrimeiroAcesso)
            {
                usuario.PrimeiroAcesso = false;
                var usuarios = JsonUserStore.Load();
                var idx = usuarios.FindIndex(u => 
                    u.Username.Equals(usuario.Username, System.StringComparison.OrdinalIgnoreCase));
                if (idx >= 0) usuarios[idx] = usuario;
                JsonUserStore.Save(usuarios);
            }

            var main = new MainWindow(usuario);
            main.Show();
            Close();
        }
    }
}
