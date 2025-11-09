namespace GLLRV.DesktopApp.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }

        public string Nome { get; set; } = "";

        public string NomeUsuario { get; set; } = "";

        // Senha armazenada como SHA256 em hexadecimal
        public string SenhaSha256Hex { get; set; } = "";

        public bool EhTecnico { get; set; }

        public int NivelTecnico { get; set; }

        // Nível de permissão geral (ex.: 1 = comum, 2 = técnico/admin)
        public int NivelPermissao { get; set; }

        public string Email { get; set; } = "";

        public string Telefone { get; set; } = "";

        // Se true, após o primeiro login obriga a cadastrar nova senha + frase
        public bool PrimeiroAcesso { get; set; } = true;

        // Frase usada para recuperação de senha
        public string FraseSeguranca { get; set; } = "";
    }
}
