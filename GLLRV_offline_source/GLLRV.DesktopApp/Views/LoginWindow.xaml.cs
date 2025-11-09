private void LoginButton_Click(object sender, RoutedEventArgs e)
{
    var username = UsernameTextBox.Text.Trim();
    var senha = PasswordBox.Password;

    var usuario = Auth.Login(username, senha);

    if (usuario == null)
    {
        MessageBox.Show("Usuário não encontrado ou senha incorreta.",
            "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
    }

    if (usuario.PrimeiroAcesso)
    {
        var first = new FirstAccessWindow(usuario)
        {
            Owner = this
        };
        first.Show();
        this.Hide();
        return;
    }

    var main = new MainWindow(usuario);
    main.Show();
    this.Close();
}
