using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itera.Suite.Application.DTOs;

public class FornecedorDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public string? Contato { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public string? TipoDeServico { get; set; }
}
