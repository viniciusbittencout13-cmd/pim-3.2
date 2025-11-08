
using System.Windows;
using GLLRV.DesktopApp.Services;

namespace GLLRV.DesktopApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        UserNameText.Text = Session.Current.Usuario?.NomeUsuario ?? "-";
    }

    private void Chamados_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Aqui entraria a tela de chamados (lista/pendentes).");
    }

    private void Sair_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
}
