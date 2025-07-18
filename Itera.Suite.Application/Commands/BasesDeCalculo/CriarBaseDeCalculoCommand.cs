namespace Itera.Suite.Application.Commands.BasesDeCalculo;

public class CriarBaseDeCalculoCommand
{
    public Guid ProjetoDeViagemId { get; set; }
    public string Nome { get; set; } = null!;
    public decimal Markup { get; set; }
}
