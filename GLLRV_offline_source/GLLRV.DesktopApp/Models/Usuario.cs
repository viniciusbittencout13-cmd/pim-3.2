namespace GLLRV.DesktopApp.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }

        public string Nome { get; set; } = "";

        // Nome de login
        public string NomeUsuario { get; set; } = "";

        // Senha em texto simples (uso acadêmico/offline)
        public string Senha { get; set; } = "";

        public bool EhTecnico { get; set; }

        public int NivelTecnico { get; set; }

        // 0 = comum, 1 = supervisor, 2 = admin, etc (ajuste como quiser)
        public int NivelPermissao { get; set; }

        public string Email { get; set; } = "";

        public string Telefone { get; set; } = "";

        public bool PrimeiroAcesso { get; set; }

        public string FraseSeguranca { get; set; } = "";

        // Área / categoria de atuação do técnico
        public string Categoria { get; set; } = "";
    }
}
