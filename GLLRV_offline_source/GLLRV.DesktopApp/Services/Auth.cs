using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp.Services
{
    public class Auth
    {
        private readonly JsonUserStore _store = new JsonUserStore();

        public Usuario? Login(string username, string password)
        {
            var user = _store.GetByUsername(username);
            if (user == null) return null;

            var hash = JsonUserStore.Sha256(password);
            if (!string.Equals(user.SenhaHash, hash, System.StringComparison.OrdinalIgnoreCase))
                return null;

            return user;
        }

        public void AtualizarPrimeiroAcesso(Usuario usuario, string novaSenha, string fraseSeguranca)
        {
            usuario.SenhaHash = JsonUserStore.Sha256(novaSenha);
            usuario.FraseSeguranca = fraseSeguranca;
            usuario.PrimeiroAcesso = false;
            _store.UpdateUsuario(usuario);
        }
    }
}
