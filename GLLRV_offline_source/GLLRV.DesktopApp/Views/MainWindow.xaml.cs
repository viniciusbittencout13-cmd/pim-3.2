using System;
using System.Windows;
using System.Windows.Controls;
using GLLRV.DesktopApp.Models;
using GLLRV.DesktopApp.Services;

namespace GLLRV.DesktopApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Children.Clear();

            var panel = new StackPanel();
            panel.Children.Add(new TextBlock
            {
                Text = "Bem-vindo!",
                FontSize = 24,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 8)
            });
            panel.Children.Add(new TextBlock
            {
                Text = "Este é o modo offline (JSON).",
                FontSize = 14
            });
            panel.Children.Add(new TextBlock
            {
                Text = "Clique em Chamados para visualizar os chamados.",
                FontSize = 14,
                Margin = new Thickness(0, 8, 0, 0)
            });
            panel.Children.Add(new TextBlock
            {
                Text = "Usuário logado: admin",
                FontSize = 14,
                Margin = new Thickness(0, 16, 0, 0)
            });

            ContentArea.Children.Add(panel);
        }

        private async void ChamadosButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Children.Clear();

            var grid = new DataGrid
            {
                AutoGenerateColumns = true,
                IsReadOnly = true,
                Margin = new Thickness(0),
            };

            ContentArea.Children.Add(grid);

            try
            {
                var store = App.DataStore;
                var chamados = await store.LoadAsync<Chamado>("chamados");
                grid.ItemsSource = chamados;
            }
            catch (Exception)
            {
                ContentArea.Children.Clear();
                ContentArea.Children.Add(new TextBlock
                {
                    Text = "Nenhum chamado encontrado. Edite o arquivo data/chamados.json para simular dados.",
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 14
                });
            }
        }

        private void SairButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
