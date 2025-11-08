
namespace GLLRV.DesktopApp.Models;
public class Chamado
{
    public int ChamadoID { get; set; }
    public int UsuarioID { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataTermino { get; set; }
    public string? Prioridade { get; set; }
    public string? Dificuldade { get; set; }
    public string? NomeTecnico { get; set; }
    public string? LocalChamado { get; set; }
    public string? HistoricoConversa { get; set; }
    public int? Satisfacao { get; set; }
    public string? TipoChamado { get; set; }
    public string? Status { get; set; }
    public string? DescricaoIA { get; set; }
}
