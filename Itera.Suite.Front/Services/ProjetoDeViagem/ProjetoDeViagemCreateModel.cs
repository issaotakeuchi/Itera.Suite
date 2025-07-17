namespace Itera.Suite.Front.Services.ProjetoDeViagem;

public class ProjetoDeViagemCreateModel
{
    public string NomeInterno { get; set; } = string.Empty;
    public Guid ClienteId { get; set; }
    public string Origem { get; set; } = string.Empty;
    public string Destino { get; set; } = string.Empty;
    public DateTime DataSaida { get; set; }     // 👈 Igual ao Command
    public DateTime DataRetorno { get; set; }   // 👈 Igual ao Command
    public string Objetivo { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string? ObservacaoInicial { get; set; }
}
