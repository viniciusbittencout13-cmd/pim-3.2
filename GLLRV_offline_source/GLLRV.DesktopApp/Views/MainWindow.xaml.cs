using System.Windows;
using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp.Views
{
    public partial class MainWindow : Window
    {
        private readonly Usuario _usuario;

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        // Esse é o construtor que recebe o usuário logado
        public MainWindow(Usuario usuario) : this()
        {
            _usuario = usuario;
            // Aqui futuramente dá pra exibir o nome e nível do técnico na interface
            // Exemplo: NomeLabel.Content = _usuario.NomeCompleto;
        }

        // Botões do menu lateral (temporariamente vazios)
        private void UsuariosButton_Click(object sender, RoutedEventArgs e) { }
        private void RelatoriosButton_Click(object sender, RoutedEventArgs e) { }
        private void ChamadosPendentesButton_Click(object sender, RoutedEventArgs e) { }
        private void HistoricoChamadosButton_Click(object sender, RoutedEventArgs e) { }
        private void ChamadosAndamentoButton_Click(object sender, RoutedEventArgs e) { }
        private void ConfiguracaoButton_Click(object sender, RoutedEventArgs e) { }

        private void SairButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
