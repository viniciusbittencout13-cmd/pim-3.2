using System;
using System.Windows;
using System.Windows.Threading;
using GLLRV.DesktopApp.Models;
using GLLRV.DesktopApp.Services;

namespace GLLRV.DesktopApp.Views
{
    public partial class MainWindow : Window
    {
        private readonly Usuario _usuario;

        public MainWindow(Usuario usuario)
        {
            InitializeComponent();

            _usuario = usuario;

            CarregarUsuarioNoTopo();
            IniciarRelogio();
            AbrirChamadosPendentes();
        }

        // Caso também exista um construtor sem parâmetros sendo usado em algum lugar
        public MainWindow()
        {
            InitializeComponent();

            _usuario = new Usuario
            {
                NomeUsuario = "vinicius",
                NomeCompleto = "Vinicius Bittencourt",
                Nivel = "Nível 2",
                Categoria = "Servidores e Gerenciamento de Rede"
            };

            CarregarUsuarioNoTopo();
            IniciarRelogio();
            AbrirChamadosPendentes();
        }

        private void CarregarUsuarioNoTopo()
        {
            if (_usuario == null) return;

            UserNameText.Text = _usuario.NomeCompleto ?? _usuario.NomeUsuario ?? "-";
            UserLevelText.Text = $"TÉCNICO: {_usuario.Nivel ?? "-"}";
            UserCategoryText.Text = $"CATEGORIA: {_usuario.Categoria ?? "-"}";

            var nome = _usuario.NomeCompleto ?? _usuario.NomeUsuario ?? "";
            var partes = nome.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var iniciais = partes.Length >= 2
                ? $"{partes[0][0]}{partes[1][0]}"
                : (partes.Length == 1 ? partes[0][0].ToString() : "?");

            InitialsText.Text = iniciais.ToUpper();
        }

        private void IniciarRelogio()
        {
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += (_, _) =>
            {
                DateTimeText.Text = DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss");
            };
            timer.Start();
        }

        private void AbrirChamadosPendentes()
        {
            // TODO: trocar pelo UserControl correto quando existir
            ContentHost.Content = new System.Windows.Controls.TextBlock
            {
                Text = "Chamados pendentes (placeholder)",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18
            };
        }

        private void UsuariosButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Content = new System.Windows.Controls.TextBlock
            {
                Text = "Usuários (placeholder)",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18
            };
        }

        private void RelatoriosButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Content = new System.Windows.Controls.TextBlock
            {
                Text = "Relatórios (placeholder)",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18
            };
        }

        private void ChamadosPendentesButton_Click(object sender, RoutedEventArgs e)
        {
            AbrirChamadosPendentes();
        }

        private void HistoricoChamadosButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Content = new System.Windows.Controls.TextBlock
            {
                Text = "Histórico de chamados (placeholder)",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18
            };
        }

        private void ChamadosAndamentoButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Content = new System.Windows.Controls.TextBlock
            {
                Text = "Chamados em andamento (placeholder)",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18
            };
        }

        private void ConfiguracaoButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Content = new System.Windows.Controls.TextBlock
            {
                Text = "Configurações (placeholder)",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18
            };
        }

        private void SairButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
