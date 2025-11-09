using System.Windows;
using GLLRV.DesktopApp.Views;

namespace GLLRV.DesktopApp
{
    public partial class App : Application
    {
       protected override void OnStartup(StartupEventArgs e)
        {
        base.OnStartup(e);
    
        var login = new Views.LoginWindow();
        login.Show();
        }

    }
}
