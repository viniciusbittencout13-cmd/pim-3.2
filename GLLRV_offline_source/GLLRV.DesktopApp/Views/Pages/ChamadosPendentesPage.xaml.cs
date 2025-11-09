using System.Collections.Generic;
using System.Windows.Controls;

namespace GLLRV.DesktopApp.Views.Pages
{
    public partial class ChamadosPendentesPage : Page
    {
        public ChamadosPendentesPage()
        {
            InitializeComponent();

            // TODO: trocar pelos dados reais do seu JSON depois.
            ChamadosGrid.ItemsSource = new List<dynamic>
            {
                new { Usuario = "usuario.teste", NivelPrioridade = "ALTA", Dificuldade = "MÉDIA", Data = "04/05/2025", Horario = "09:00" }
            };
        }
    }
}
