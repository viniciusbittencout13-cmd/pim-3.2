using System;

namespace GLLRV.DesktopApp.Models
{
    public class Usuario
    {
        // Nome de login
        public string Username { get; set; } = "";

        // Senha armazenada como SHA256 em hexadecimal
        public string Senha { get; set; } = "";

        // Nível do técnico (ex: 1, 2, 3)
        public int Nivel { get; set; }

        // Atribuições / categoria do técnico
        public string Atribuicoes { get; set; } = "";

        // Se true, obriga passar pela tela de primeiro acesso
        public bool PrimeiroAcesso { get; set; } = true;

        // Frase de segurança para recuperação de senha
        public string FraseSeguranca { get; set; } = "";
    }
}
