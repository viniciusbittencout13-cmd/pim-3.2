
namespace GLLRV.DesktopApp.Models;
public class Usuario
{
    public int UsuarioID { get; set; }
    public string Nome { get; set; } = "";
    public string NomeUsuario { get; set; } = "";
    public string SenhaSha256Hex { get; set; } = ""; // hash em hex
    public bool EhTecnico { get; set; }
    public int? NivelTecnico { get; set; }
    public string? CategoriaAtendimento { get; set; }
    public int? NivelPermissao { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
}
