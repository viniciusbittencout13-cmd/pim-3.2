using System.Security.Cryptography;
using System.Text;
using System.Windows;
using GLLRV.DesktopApp.Models;
using GLLRV.DesktopApp.Services;

namespace GLLRV.DesktopApp.Views
{
    public partial class FirstAccessWindow : Window
    {
        private readonly Usuario _usuario;
        private readonly JsonUserStore _userStore;

        public FirstAccessWindow(Usuario usuario, JsonUserStore userStore)
        {
            InitializeComponent();
            _usuario = usuario;
            _userStore = userStore;
        }

        private static string Sha256(string input)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sb = new StringBuilder();
            foreach (var b in bytes)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        private async void ConfirmarButton_Click(object sender, RoutedEventArgs e)
        {
            var novaSenha = NewPasswordBox.Password;
            var repeteSenha = ConfirmPasswordBox.Password;
            var frase = SecurityPhraseTextBox.Text?.Trim();

            if (string.IsNullOrWhiteSpace(novaSenha) ||
                string.IsNullOrWhiteSpace(repeteSenha) ||
                string.IsNullOrWhiteSpace(frase))
            {
                MessageBox.Show("Preencha todos os campos.", "Atenção",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (novaSenha != repeteSenha)
            {
                MessageBox.Show("As senhas não conferem.", "Atenção",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _usuario.SenhaHash = Sha256(novaSenha);
            _usuario.FraseSeguranca = frase;
            _usuario.PrimeiroAcessoConcluido = true;

            await _userStore.AtualizarAsync(_usuario);

            MessageBox.Show("Senha definida com sucesso!", "Sucesso",
                MessageBoxButton.OK, MessageBoxImage.Information);

            var main = new MainWindow(_usuario);
            main.Show();
            Close();
        }
    }
}
