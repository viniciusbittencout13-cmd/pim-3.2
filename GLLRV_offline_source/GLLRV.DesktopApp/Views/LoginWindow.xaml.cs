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
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _usuario = usuario;
        }

        private void ConfirmarButton_Click(object sender, RoutedEventArgs e)
        {
            var frase = SecurityPhraseTextBox.Text.Trim();
            var novaSenha = NewPasswordBox.Password;
            var repetir = ConfirmPasswordBox.Password;

            if (string.IsNullOrWhiteSpace(frase) ||
                string.IsNullOrWhiteSpace(novaSenha) ||
                string.IsNullOrWhiteSpace(repetir))
            {
                MessageBox.Show("Preencha todos os campos.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (novaSenha != repetir)
            {
                MessageBox.Show("As senhas não conferem.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _usuario.Senha = novaSenha;          // direto, sem hash
            _usuario.FraseSeguranca = frase;
            _usuario.PrimeiroAcesso = false;

            Auth.AtualizarUsuario(_usuario);

            var main = new MainWindow(_usuario);
            main.Show();
            Close();
        }
    }
}
