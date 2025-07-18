namespace Itera.Suite.Application.Commands.BasesDeCalculo;

public class DefinirQuantidadesDaBaseCommand
{
    public Guid BaseId { get; set; }
    public int QtdAlunos { get; set; }
    public int QtdProfessores { get; set; }
    public int QtdGuias { get; set; }
    public int QtdMotoristas { get; set; }
    public bool MotoristaPermanece { get; set; }
}
