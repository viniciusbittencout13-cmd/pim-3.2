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
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            var frase = SecurityPhraseTextBox.Text.Trim();
            var novaSenha = NewPasswordBox.Password;
            var repeteSenha = ConfirmPasswordBox.Password;

            if (string.IsNullOrWhiteSpace(frase) ||
                string.IsNullOrWhiteSpace(novaSenha) ||
                string.IsNullOrWhiteSpace(repeteSenha))
            {
                MessageBox.Show("Preencha todos os campos.", "Aviso",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (novaSenha != repeteSenha)
            {
                MessageBox.Show("As senhas não coincidem.", "Erro",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _usuario.FraseSeguranca = frase;
            _usuario.Senha = novaSenha;
            _usuario.PrimeiroAcesso = false;

            JsonUserStore.UpdateUser(_usuario);

            MessageBox.Show("Senha atualizada com sucesso.", "Informação",
                MessageBoxButton.OK, MessageBoxImage.Information);

            var main = new MainWindow(_usuario);
            main.Show();
            Close();
        }
    }
}
