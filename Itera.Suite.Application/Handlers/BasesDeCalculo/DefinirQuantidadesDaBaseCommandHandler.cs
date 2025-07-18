using Itera.Suite.Application.Commands.BasesDeCalculo;
using Itera.Suite.Domain.Interfaces;

namespace Itera.Suite.Application.Handlers.BasesDeCalculo;

public class DefinirQuantidadesDaBaseCommandHandler
{
    private readonly IBaseDeCalculoRepository _repository;

    public DefinirQuantidadesDaBaseCommandHandler(IBaseDeCalculoRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(DefinirQuantidadesDaBaseCommand command)
    {
        var baseCalculo = await _repository.ObterPorIdAsync(command.BaseId)
            ?? throw new InvalidOperationException("Base de cálculo não encontrada.");

        baseCalculo.DefinirQuantidades(
            alunos: command.QtdAlunos,
            professores: command.QtdProfessores,
            guias: command.QtdGuias,
            motoristas: command.QtdMotoristas,
            motoristaPermanece: command.MotoristaPermanece
        );

        await _repository.SalvarAlteracoesAsync();
    }
}
