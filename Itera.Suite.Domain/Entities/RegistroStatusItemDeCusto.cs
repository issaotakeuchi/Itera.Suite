using Itera.Suite.Shared.Enums;

namespace Itera.Suite.Domain.Entities;

public class RegistroStatusItemDeCusto
{
    public Guid Id { get; set; }
    public Guid ItemDeCustoId { get; set; }
    public StatusItemDeCusto Status { get; set; }
    public DateTime DataHora { get; set; }
    public string UsuarioResponsavel { get; set; }
    public string? Justificativa { get; set; }
    public string? ArquivoUrl { get; set; }
}
