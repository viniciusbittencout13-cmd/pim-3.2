using System;
using System.IO;
using System.Windows;
using GLLRV.DesktopApp.Views;
using GLLRV.DesktopApp.Services;
using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp
{
    public partial class App : Application
    {
        public static string DataFolderPath =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Garante que a pasta existe e que há um usuário padrão
            Auth.CarregarUsuarios(); // se não existir, cria com vinicius/admin

            var login = new LoginWindow();
            login.Show();
        }
    }
}
