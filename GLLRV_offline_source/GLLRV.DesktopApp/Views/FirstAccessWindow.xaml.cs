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

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            var frase = SecurityPhraseTextBox.Text.Trim();
            var novaSenha = NewPasswordBox.Password.Trim();
            var confirma = ConfirmPasswordBox.Password.Trim();

            if (string.IsNullOrWhiteSpace(frase))
            {
                MessageBox.Show("Informe uma frase de segurança.");
                return;
            }

            if (string.IsNullOrWhiteSpace(novaSenha))
            {
                MessageBox.Show("Informe a nova senha.");
                return;
            }

            if (novaSenha != confirma)
            {
                MessageBox.Show("As senhas não conferem.");
                return;
            }

            _usuario.Senha = novaSenha;
            _usuario.FraseSeguranca = frase;
            _usuario.PrimeiroAcesso = false;

            JsonUserStore.UpdateUser(_usuario);

            MessageBox.Show("Dados atualizados com sucesso.");
            DialogResult = true;
            Close();
        }
    }
}
