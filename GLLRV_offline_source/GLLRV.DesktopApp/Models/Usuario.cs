namespace GLLRV.DesktopApp.Models
{
    public class Usuario
    {
        public string Username { get; set; } = "";
        public string Senha { get; set; } = "";
        public int Nivel { get; set; }
        public string Atribuicoes { get; set; } = "";
        public bool PrimeiroAcesso { get; set; } = true;
    }
}
