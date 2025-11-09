using System.Windows;
using GLLRV.DesktopApp.Services;
using GLLRV.DesktopApp.Views;

namespace GLLRV.DesktopApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Gera data/usuarios/usuarios.json com usuário padrão, se não existir
            JsonUserStore.EnsureSeedUser();

            // Abre tela de login em vez de cair direto no sistema
            var login = new LoginWindow();
            login.Show();
        }
    }
}
