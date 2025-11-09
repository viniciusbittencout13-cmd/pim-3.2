using System.Windows;
using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp
{
    public partial class MainWindow : Window
    {
        private readonly Usuario? _usuario;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(Usuario usuario) : this()
        {
            _usuario = usuario;

            // Se você tiver labels no XAML para mostrar os dados,
            // pode usar algo assim (só se os controles existirem):
            //
            // NomeTecnicoLabel.Content = _usuario.Nome;
            // NivelTecnicoLabel.Content = $"TÉCNICO :  NÍVEL {_usuario.NivelTecnico}";
            // CategoriaLabel.Content   = $"CATEGORIA: {_usuario.Categoria}";
        }
    }
}
