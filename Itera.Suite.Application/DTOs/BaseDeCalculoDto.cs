using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itera.Suite.Application.DTOs;

public class BaseDeCalculoDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public decimal Markup { get; set; }
    public int QtdAlunos { get; set; }
    public int QtdProfessores { get; set; }
    public int QtdGuias { get; set; }
    public int QtdMotoristas { get; set; }
    public bool MotoristaPermanece { get; set; }
    public decimal Total { get; set; }
    public decimal TotalComMarkup { get; set; }

    public List<ItemDeCustoAplicadoDto> Itens { get; set; } = new();
}
