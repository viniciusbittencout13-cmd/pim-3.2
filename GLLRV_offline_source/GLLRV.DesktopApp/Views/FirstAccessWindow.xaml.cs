using System.Windows;
using GLLRV.DesktopApp.Models;
using GLLRV.DesktopApp.Services;

namespace GLLRV.DesktopApp.Views
{
    public partial class FirstAccessWindow : Window
    {
        private readonly Usuario _usuario;
        private readonly Auth _auth;

        public FirstAccessWindow(Usuario usuario, Auth auth)
        {
            InitializeComponent();
            _usuario = usuario;
            _auth = auth;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            var frase = SecurityPhraseTextBox.Text.Trim();
            var novaSenha = NewPasswordBox.Password;
            var confirma = ConfirmPasswordBox.Password;

            if (string.IsNullOrWhiteSpace(novaSenha) || novaSenha != confirma)
            {
                MessageBox.Show("As senhas não coincidem.", "Erro",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _auth.AtualizarPrimeiroAcesso(_usuario, novaSenha, frase);

            MessageBox.Show("Senha e frase de segurança atualizadas.", "Sucesso",
                MessageBoxButton.OK, MessageBoxImage.Information);

            DialogResult = true;
            Close();
        }
    }
}
