using System;
using System.Linq;
using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp.Services
{
    public static class Auth
    {
        public static Usuario? Login(string username, string password)
        {
            var usuarios = JsonUserStore.LoadAll();

            // compara username sem case, senha igual
            var usuario = usuarios.FirstOrDefault(u =>
                string.Equals(u.NomeUsuario, username, StringComparison.OrdinalIgnoreCase) &&
                u.Senha == password);

            return usuario;
        }

        public static void AtualizarUsuario(Usuario usuarioAtualizado)
        {
            var usuarios = JsonUserStore.LoadAll();

            var idx = usuarios.FindIndex(u =>
                string.Equals(u.NomeUsuario, usuarioAtualizado.NomeUsuario, StringComparison.OrdinalIgnoreCase));

            if (idx >= 0)
            {
                usuarios[idx] = usuarioAtualizado;
                JsonUserStore.SaveAll(usuarios);
            }
        }
    }
}
