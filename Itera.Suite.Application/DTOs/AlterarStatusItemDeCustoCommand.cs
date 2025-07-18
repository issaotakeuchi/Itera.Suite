using Itera.Suite.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itera.Suite.Application.DTOs;

public class AlterarStatusItemDeCustoCommand
{
    public Guid Id { get; set; }
    public StatusItemDeCusto NovoStatus { get; set; }
    public string? Justificativa { get; set; }
}
