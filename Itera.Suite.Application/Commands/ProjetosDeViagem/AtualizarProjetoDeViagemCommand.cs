namespace Itera.Suite.Application.Commands.ProjetosDeViagem;

public class AtualizarProjetoDeViagemCommand
{
    public Guid Id { get; set; }
    public string? NomeInterno { get; set; }
    public string? Origem { get; set; }
    public string? Destino { get; set; }
    public string? Objetivo { get; set; }
    public DateTime? DataSaida { get; set; }
    public DateTime? DataRetorno { get; set; }
    public string? Tipo { get; set; }
    public string? Status { get; set; }
}
