namespace Itera.Suite.Application.Commands.ProjetosDeViagem;

public class CriarProjetoDeViagemCommand
{
    public string NomeInterno { get; set; } = null!;
    public Guid ClienteId { get; set; }
    public string Origem { get; set; } = null!;
    public string Destino { get; set; } = null!;
    public DateTime DataSaida { get; set; }  // ⚡ Vai virar DateOnly no Handler
    public DateTime DataRetorno { get; set; } // ⚡ Idem
    public string Objetivo { get; set; } = null!;
    public string Tipo { get; set; } = null!;  // ⚡ Vai virar Enum no Handler
    public string? ObservacaoInicial { get; set; }  // Opcional, perfeito
}
