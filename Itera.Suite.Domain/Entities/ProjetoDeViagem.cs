using Itera.Suite.Domain.Enums;
using Itera.Suite.Domain.ValueObjects;

namespace Itera.Suite.Domain.Entities;

public class ProjetoDeViagem
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public Cliente Cliente { get; set; }
    public string NomeInterno { get; set; }      // PETAR 2024 - Escola Lumiar
    public TipoProjeto Tipo { get; set; }             // Grupo, FIT, Corporativa
    public string Origem { get; set; }           // Escola, cliente, etc.
    public string Destino { get; set; }
    public DateTime DataSaida { get; set; }
    public DateTime DataRetorno { get; set; }
    public string Objetivo { get; set; }         // Ex: vivência pedagógica
    public StatusProjeto Status { get; set; }           // Planejado, Em execução, Concluído, Cancelado
    public ICollection<ItemDeCusto> ItensDeCusto { get; set; } = new List<ItemDeCusto>();
    public ICollection<ObservacaoProjeto> Observacoes { get; set; } = new List<ObservacaoProjeto>();

}
