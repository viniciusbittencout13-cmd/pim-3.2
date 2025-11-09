using System;

namespace GLLRV.DesktopApp.Models
{
    public class Usuario
    {
        public string NomeUsuario { get; set; } = "";
        public string Senha { get; set; } = "";              // senha em texto simples (offline, demo)
        public string NomeCompleto { get; set; } = "";
        public string Nivel { get; set; } = "";
        public string Categoria { get; set; } = "";
        public bool PrimeiroAcesso { get; set; } = true;
        public string FraseSeguranca { get; set; } = "";
    }
}
