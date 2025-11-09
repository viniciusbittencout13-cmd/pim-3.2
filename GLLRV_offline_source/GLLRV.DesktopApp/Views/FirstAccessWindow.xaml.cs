using System;
using System.IO;
using System.Linq;
using System.Text.Json;
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
            var repetir = ConfirmPasswordBox.Password;

            if (string.IsNullOrWhiteSpace(frase) ||
                string.IsNullOrWhiteSpace(novaSenha) ||
                string.IsNullOrWhiteSpace(repetir))
            {
                MessageBox.Show("Preencha todos os campos.", "Aviso",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (novaSenha != repetir)
            {
                MessageBox.Show("As senhas não conferem.", "Erro",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Atualiza usuário
            _usuario.Senha = novaSenha;
            _usuario.FraseSeguranca = frase;
            _usuario.PrimeiroAcesso = false;

            // Carrega lista, atualiza e salva
            var usuarios = Auth.CarregarUsuarios();
            var existente = usuarios.FirstOrDefault(u => u.UsuarioID == _usuario.UsuarioID);

            if (existente != null)
            {
                var idx = usuarios.IndexOf(existente);
                usuarios[idx] = _usuario;
            }
            else
            {
                usuarios.Add(_usuario);
            }

            Auth.SalvarUsuarios(usuarios);

            MessageBox.Show("Primeiro acesso configurado com sucesso!", "Sucesso",
                MessageBoxButton.OK, MessageBoxImage.Information);

            var main = new MainWindow(_usuario);
            main.Show();

            Owner?.Close();
            Close();
        }
    }
}
