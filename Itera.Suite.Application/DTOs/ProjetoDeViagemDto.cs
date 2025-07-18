public class ProjetoDeViagemDto
{
    public Guid Id { get; set; }
    public string NomeInterno { get; set; } = null!;
    public string Origem { get; set; } = null!;
    public string Destino { get; set; } = null!;
    public string Objetivo { get; set; } = null!;
    public string Tipo { get; set; } = null!;
    public string Status { get; set; } = null!;
    public DateOnly DataSaida { get; set; }
    public DateOnly DataRetorno { get; set; }
    public string ClienteNome { get; set; } = null!;
    public Guid ClienteId { get; set; }
    public decimal ValorTotalProvisionado { get; set; } // total da base confirmada
}
