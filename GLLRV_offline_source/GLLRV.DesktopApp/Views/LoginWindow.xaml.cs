using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using GLLRV.DesktopApp.Models;
using GLLRV.DesktopApp.Services;

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
            var password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Informe usuário e senha.", "Aviso",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var usuarios = CarregarOuCriarUsuarios();
            var usuario = usuarios.FirstOrDefault(u =>
                   u.NomeUsuario.Equals(username, StringComparison.OrdinalIgnoreCase)
                || u.Nome.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (usuario == null)
            {
                MessageBox.Show("Usuário não encontrado.", "Erro",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var hashDigitado = Auth.Sha256Hex(password);

            if (usuario.PrimeiroAcesso)
            {
                // Primeiro acesso: senha precisa bater com a atual (admin na primeira vez)
                if (hashDigitado != usuario.SenhaSha256Hex)
                {
                    MessageBox.Show("Senha incorreta.", "Erro",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var first = new FirstAccessWindow(usuario);
                first.Owner = this;
                Hide();
                first.Show();
                return;
            }

            // Acesso normal
            if (hashDigitado != usuario.SenhaSha256Hex)
            {
                MessageBox.Show("Usuário ou senha inválidos.", "Erro",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var main = new MainWindow();
            main.Show();
            Close();
        }

        /// <summary>
        /// Carrega usuarios.json. Se não existir, cria o padrão com Vinicius.
        /// </summary>
        private Usuario[] CarregarOuCriarUsuarios()
        {
            var basePath = AppContext.BaseDirectory;
            var dataFolder = App.Configuration["DataFolder"] ?? "data";
            var dir = Path.Combine(basePath, dataFolder);
            Directory.CreateDirectory(dir);

            var path = Path.Combine(dir, "usuarios.json");

            if (!File.Exists(path))
            {
                // Cria usuário padrão Vinicius
                var vinicius = new Usuario
                {
                    UsuarioID = 1,
                    Nome = "Vinicius Bittencourt",
                    NomeUsuario = "vinicius",
                    SenhaSha256Hex = Auth.Sha256Hex("admin"),
                    EhTecnico = true,
                    NivelTecnico = 2,
                    NivelPermissao = 2,
                    Email = "vinicius@gllrv.local",
                    Telefone = "(11) 99999-0001",
                    PrimeiroAcesso = true
                };

                var jsonNovo = JsonSerializer.Serialize(
                    new[] { vinicius },
                    new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                File.WriteAllText(path, jsonNovo);
                return new[] { vinicius };
            }

            var json = File.ReadAllText(path);
            var lista = JsonSerializer.Deserialize<Usuario[]>(json);
            return lista ?? Array.Empty<Usuario>();
        }
    }
}
