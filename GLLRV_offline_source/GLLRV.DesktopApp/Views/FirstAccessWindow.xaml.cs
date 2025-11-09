using System.Windows;
using GLLRV.DesktopApp.Models;
using GLLRV.DesktopApp.Services;

namespace GLLRV.DesktopApp.Views
{
    public partial class FirstAccessWindow : Window
    {
        private readonly Usuario _user;

        public FirstAccessWindow(Usuario user)
        {
            InitializeComponent();
            _user = user;
        }

        private void ConfirmarButton_Click(object sender, RoutedEventArgs e)
        {
            var frase = SecurityPhraseTextBox.Text?.Trim() ?? "";
            var senha1 = NewPasswordBox.Password;
            var senha2 = ConfirmPasswordBox.Password;

            if (string.IsNullOrWhiteSpace(senha1))
            {
                MessageBox.Show("Informe a nova senha.");
                return;
            }

            if (senha1 != senha2)
            {
                MessageBox.Show("As senhas não conferem.");
                return;
            }

            _user.Senha = Auth.Sha256Hex(senha1);
            _user.PrimeiroAcesso = false;
            _user.FraseSeguranca = frase;

            JsonUserStore.UpdateUser(_user);

            var main = new MainWindow();
            main.Show();
            Close();
        }
    }
}
