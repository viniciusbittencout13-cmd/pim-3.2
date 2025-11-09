using System;
using System.Windows;
using System.Windows.Threading;
using GLLRV.DesktopApp.Models;
using GLLRV.DesktopApp.Services;
using GLLRV.DesktopApp.Views.Pages; // vamos criar essas páginas já já

namespace GLLRV.DesktopApp.Views
{
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer _timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            CarregarUsuarioLogado();
            IniciarRelogio();
            AbrirChamadosPendentes();
        }

        private void CarregarUsuarioLogado()
        {
            var usuario = Auth.UsuarioLogado; // usa o que já temos do login

            if (usuario != null)
            {
                UserNameText.Text = usuario.NomeUsuario;
                UserLevelText.Text = $"TECNICO :  NÍVEL {usuario.Nivel}";
                UserCategoryText.Text = $"CATEGORIA: {usuario.Atribuicoes}";

                InitialsText.Text = ObterIniciais(usuario.NomeUsuario);
            }
            else
            {
                UserNameText.Text = "-";
                UserLevelText.Text = "";
                UserCategoryText.Text = "";
                InitialsText.Text = "?";
            }
        }

        private static string ObterIniciais(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return "?";

            var partes = nome.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (partes.Length == 1)
                return partes[0][0].ToString().ToUpper();

            return (partes[0][0].ToString() + partes[^1][0]).ToUpper();
        }

        private void IniciarRelogio()
        {
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (s, e) =>
            {
                DateTimeText.Text = DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss");
            };
            _timer.Start();
        }

        private void AbrirChamadosPendentes()
        {
            ContentHost.Content = new ChamadosPendentesPage();
        }

        private void UsuariosButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Content = new UsuariosPage();
        }

        private void RelatoriosButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Content = new RelatoriosPage();
        }

        private void ChamadosPendentesButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Content = new ChamadosPendentesPage();
        }

        private void HistoricoChamadosButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Content = new HistoricoChamadosPage();
        }

        private void ChamadosAndamentoButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Content = new ChamadosAndamentoPage();
        }

        private void ConfiguracaoButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Content = new ConfiguracoesPage();
        }

        private void SairButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
