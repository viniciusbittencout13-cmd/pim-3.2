using System;
using System.Linq;
using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp.Services
{
    public static class Auth
    {
        public static Usuario Login(string username, string senha)
        {
            var usuarios = JsonUserStore.LoadUsers();

            return usuarios.FirstOrDefault(u =>
                u.NomeUsuario.Equals(username, StringComparison.OrdinalIgnoreCase)
                && u.Senha == senha);
        }
    }
}
