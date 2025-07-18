using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itera.Suite.Application.DTOs;

public class BaseDeCalculoResumoDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public decimal Total { get; set; }
    public bool Confirmada { get; set; }
}
