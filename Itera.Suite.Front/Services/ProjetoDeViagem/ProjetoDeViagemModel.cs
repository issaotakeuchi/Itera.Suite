namespace Itera.Suite.Front.Services.ProjetoDeViagem;

public class ProjetoDeViagemModel
{
    public Guid Id { get; set; }
    public string NomeInterno { get; set; } = string.Empty;
    public string Origem { get; set; } = string.Empty;
    public string Destino { get; set; } = string.Empty;
    public string Objetivo { get; set; } = string.Empty;
    public string Tipo { get; set; } = "Grupo";
    public string Status { get; set; } = string.Empty;
    public DateOnly DataSaida { get; set; }
    public DateOnly DataRetorno { get; set; }
    public string ClienteNome { get; set; } = string.Empty;
}
