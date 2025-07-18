using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itera.Suite.Application.DTOs;

public class ItemDeCustoAplicadoDto
{
    public Guid Id { get; set; }
    public string? Subtipo { get; set; }
    public int QuantidadeTotal { get; set; }
    public int QuantidadeCortesia { get; set; }
    public decimal ValorEditado { get; set; }
    public decimal Total { get; set; }
    public string? Fornecedor { get; set; }
    public string Categoria { get; set; } = null!;
    public string? Descricao { get; set; }
}
