using System;
using System.Windows;
using System.Windows.Threading;
using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp.Views
{
    public partial class MainWindow : Window
    {
        private readonly Usuario _usuario;

        // Construtor chamado após login (com usuário carregado)
        public MainWindow(Usuario usuario)
        {
            InitializeComponent();

            _usuario = usuario ?? new Usuario
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

        // Construtor padrão (fallback)
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

            string iniciais = "?";
            if (partes.Length >= 2)
                iniciais = $"{partes[0][0]}{partes[1][0]}";
            else if (partes.Length == 1)
                iniciais = partes[0][0].ToString();

            InitialsText.Text = iniciais.ToUpperInvariant();
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
            ContentHost.Content = new System.Windows.Controls.TextBlock
            {
                Text = "Chamados pendentes (tela em construção)",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18
            };
        }

        private void UsuariosButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Content = new System.Windows.Controls.TextBlock
            {
                Text = "Usuários (tela em construção)",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18
            };
        }

        private void RelatoriosButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Content = new System.Windows.Controls.TextBlock
            {
                Text = "Relatórios (tela em construção)",
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
                Text = "Histórico de chamados (tela em construção)",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18
            };
        }

        private void ChamadosAndamentoButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Content = new System.Windows.Controls.TextBlock
            {
                Text = "Chamados em andamento (tela em construção)",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18
            };
        }

        private void ConfiguracaoButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Content = new System.Windows.Controls.TextBlock
            {
                Text = "Configurações (tela em construção)",
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
