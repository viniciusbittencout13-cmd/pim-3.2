using System.Windows;
using System.Windows.Controls;

namespace GLLRV.DesktopApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Já inicia na aba de Chamados Pendentes
            ChamadosPendentesButton_Click(null, null);
        }

        private void SetPage(UIElement conteudo)
        {
            ContentArea.Children.Clear();
            ContentArea.Children.Add(conteudo);
        }

        private void UsuariosButton_Click(object sender, RoutedEventArgs e)
        {
            var panel = new StackPanel();
            panel.Children.Add(new TextBlock
            {
                Text = "Usuários",
                FontSize = 22,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 12)
            });
            panel.Children.Add(new TextBlock
            {
                Text = "Área de cadastro e edição de usuários (modo offline).",
                FontSize = 14
            });
            SetPage(panel);
        }

        private void RelatoriosButton_Click(object sender, RoutedEventArgs e)
        {
            var panel = new StackPanel();
            panel.Children.Add(new TextBlock
            {
                Text = "Relatórios",
                FontSize = 22,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 12)
            });
            panel.Children.Add(new TextBlock
            {
                Text = "Relatórios de desempenho e avaliação dos atendimentos.",
                FontSize = 14
            });
            SetPage(panel);
        }

        private void ChamadosPendentesButton_Click(object sender, RoutedEventArgs e)
        {
            var panel = new StackPanel();
            panel.Children.Add(new TextBlock
            {
                Text = "Chamados Pendentes",
                FontSize = 22,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 12)
            });
            panel.Children.Add(new TextBlock
            {
                Text = "Lista de chamados que ainda não foram atendidos.",
                FontSize = 14
            });
            SetPage(panel);
        }

        private void HistoricoChamadosButton_Click(object sender, RoutedEventArgs e)
        {
            var panel = new StackPanel();
            panel.Children.Add(new TextBlock
            {
                Text = "Histórico de Chamados",
                FontSize = 22,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 12)
            });
            panel.Children.Add(new TextBlock
            {
                Text = "Chamados finalizados e arquivados.",
                FontSize = 14
            });
            SetPage(panel);
        }

        private void ChamadosAndamentoButton_Click(object sender, RoutedEventArgs e)
        {
            var panel = new StackPanel();
            panel.Children.Add(new TextBlock
            {
                Text = "Chamados em Andamento",
                FontSize = 22,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 12)
            });
            panel.Children.Add(new TextBlock
            {
                Text = "Chamados atualmente sendo atendidos.",
                FontSize = 14
            });
            SetPage(panel);
        }

        private void ConfiguracaoButton_Click(object sender, RoutedEventArgs e)
        {
            var panel = new StackPanel();
            panel.Children.Add(new TextBlock
            {
                Text = "Configurações",
                FontSize = 22,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 12)
            });
            panel.Children.Add(new TextBlock
            {
                Text = "Preferências do sistema e dados do usuário logado.",
                FontSize = 14
            });
            SetPage(panel);
        }

        private void SairButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
