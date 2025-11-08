
using System.Windows;
using GLLRV.DesktopApp.Models;
using GLLRV.DesktopApp.Services;

namespace GLLRV.DesktopApp.Views;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
    }

    private async void Entrar_Click(object sender, RoutedEventArgs e)
    {
        var usuarios = await App.DataStore.LoadAsync<Usuario>("usuarios");
        var u = usuarios.FirstOrDefault(x => x.NomeUsuario.Equals(UserNameBox.Text, StringComparison.OrdinalIgnoreCase));
        if (u is null || !Auth.VerifyPasswordHex(PasswordBox.Password, u.SenhaSha256Hex))
        {
            MessageBox.Show("Usuário ou senha inválidos");
            return;
        }
        Session.Current.Usuario = u;
        var main = new MainWindow();
        main.Show();
        Close();
    }
}
